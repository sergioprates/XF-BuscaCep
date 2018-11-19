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
        {

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

        private Command _buscarCommand;

        public Command BuscarCommand => _buscarCommand ?? (_buscarCommand = new Command(async () => await BuscarCommandExecute()));

        async Task BuscarCommandExecute()
        {
            try
            {
                string resultado = await Clients.ViaCepHttpClient.Current.BuscarCep(_cep);

                if (!string.IsNullOrWhiteSpace(resultado))
                    await App.Current.MainPage.DisplayAlert("Vish...", resultado, "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta!", ex.Message, "OK");
            }
        }
    }
}
