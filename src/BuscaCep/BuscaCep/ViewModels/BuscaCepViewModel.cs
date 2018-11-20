using BuscaCep.Clients;
using BuscaCep.Cross;
using BuscaCep.Data.Entidades;
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

        private Cep _cep;
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
        
        public string CEP => _cep?.CEP;

        public string Logradouro => _cep?.Logradouro;
        public string Bairro=> _cep?.Bairro;

        public string Localidade => _cep?.Localidade;

        public string UF
        {
            get => _cep?.Uf;
        }

        public bool HasCep { get => _cep != null; }

        private Command _buscarCommand;
        public Command BuscarCommand => _buscarCommand ?? (_buscarCommand = new Command(async () => await BuscarCommandExecute(), () => IsNotBusy));

        async Task BuscarCommandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                this.TratarCommandCanExecute(BuscarCommand, true);
                this.TratarCommandCanExecute(AdicionarCommand, true);

                BuscaCepResult consulta = await Clients.ViaCepHttpClient.Current.BuscarCep(_cepBusca);

                this._cep = this.ConverterParaCep(consulta);
                this.TratarOnPropertyChange();
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

        private void TratarOnPropertyChange()
        {
            this.OnPropertyChanged(nameof(this.Bairro));
            this.OnPropertyChanged(nameof(this.CEP));
            this.OnPropertyChanged(nameof(this.HasCep));
            this.OnPropertyChanged(nameof(this.Localidade));
            this.OnPropertyChanged(nameof(this.Logradouro));
            this.OnPropertyChanged(nameof(this.UF));
        }

        private Cep ConverterParaCep(BuscaCepResult consulta)
        {
            return new Cep()
            {
                Id = Guid.NewGuid(),
                Bairro = consulta.bairro,
                CEP = consulta.cep,
                Complemento = consulta.complemento,
                Gia = consulta.gia,
                Ibge = consulta.ibge,
                Localidade = consulta.localidade,
                Logradouro = consulta.logradouro,
                Uf = consulta.uf,
                Unidade = consulta.gia
            };
        }

        private void TratarCommandCanExecute(Command command, bool isBusy)
        {
            IsBusy = isBusy;
            command.ChangeCanExecute();
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

                Data.DatabaseService.Current.Salvar(_cep);

                MessagingCenter.Send(this, MessageKeys.CepsAtualizados);

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
