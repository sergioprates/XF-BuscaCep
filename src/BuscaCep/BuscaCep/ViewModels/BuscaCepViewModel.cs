using BuscaCep.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuscaCep.ViewModels
{
    class BuscaCepViewModel : ViewModelBase
    {
        public BuscaCepViewModel()
            : base()
        { }

        private string _cepBusca;

        public string CEPBusca
        {
            get => _cepBusca;
            set
            {
                _cepBusca = value;
                OnPropertyChanged();
            }
        }

        private string _cep;

        public string CEP
        {
            get => _cep;
            set
            {
                _cep = value;
                OnPropertyChanged();
            }
        }

        private string _logradouro;

        public string Logradouro
        {
            get => _logradouro;
            set
            {
                _logradouro = value;
                OnPropertyChanged();
            }
        }

        private string _bairro;

        public string Bairro
        {
            get => _bairro;
            set
            {
                _bairro = value;
                OnPropertyChanged();
            }
        }

        private string _localidade;

        public string Localidade
        {
            get => _localidade;
            set
            {
                _localidade = value;
                OnPropertyChanged();
            }
        }

        private string _uf;

        public string UF
        {
            get => _uf;
            set
            {
                _uf = value;
                OnPropertyChanged();
            }
        }

        public bool HasCep { get => !string.IsNullOrWhiteSpace(_cep); }

        private Command _buscarCommand;
        public Command BuscarCommand => _buscarCommand ?? (_buscarCommand = new Command(async () => await BuscarCommandExecute(), ()=> IsNotBusy));

        async Task BuscarCommandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                this.TratarCommandCanExecute(BuscarCommand, true);
                this.TratarCommandCanExecute(AdicionarCommand, true);

                BuscaCepResult consulta = await Clients.ViaCepHttpClient.Current.BuscarCep(_cepBusca);

                this.AtribuirValores(consulta);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta!", ex.Message, "OK");
            }
            finally
            {
                this.TratarCommandCanExecute(BuscarCommand, false);
                this.TratarCommandCanExecute(AdicionarCommand, false);
            }
        }

        private void TratarCommandCanExecute(Command command, bool isBusy)
        {
            IsBusy = isBusy;
            command.ChangeCanExecute();
        }

        private void AtribuirValores(BuscaCepResult consulta)
        {
            this.CEP = consulta.cep;

            if (this.HasCep)
            {
                this.Bairro = consulta.bairro;
                this.Localidade = consulta.localidade;
                this.Logradouro = consulta.logradouro;
                this.UF = consulta.uf;
            }

            OnPropertyChanged(nameof(HasCep));
        }

        private Command _adicionarCommand;

        public Command AdicionarCommand => _adicionarCommand ?? (_adicionarCommand = new Command(async () => await AdicionarCommandExecute(), () => IsNotBusy));

        async Task AdicionarCommandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                this.TratarCommandCanExecute(AdicionarCommand, true);
                this.TratarCommandCanExecute(BuscarCommand, true);

                MessagingCenter.Send(this, "ADICIONAR CEP");

                await base.PopAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta!", ex.Message, "OK");
            }
            finally
            {
                this.TratarCommandCanExecute(AdicionarCommand, false);
                this.TratarCommandCanExecute(BuscarCommand, false);
            }
        }
    }
}
