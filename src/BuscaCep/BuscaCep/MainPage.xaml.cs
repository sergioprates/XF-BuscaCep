using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuscaCep
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnBuscarCep_Clicked(object sender, EventArgs e)
        {
            try
            {
                string resultado = await Clients.ViaCepHttpClient.Current.BuscarCep(txtCep.Text);

                if (!string.IsNullOrWhiteSpace(resultado))
                    await DisplayAlert("Vish...", resultado, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta!", ex.Message, "OK");
            }
        }
    }
}
