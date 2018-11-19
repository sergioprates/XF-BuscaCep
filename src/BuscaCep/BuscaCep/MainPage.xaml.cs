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
                if (string.IsNullOrWhiteSpace(txtCep.Text))
                    throw new InvalidOperationException("Informe o CEP.");

                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync($"http://viacep.com.br/ws/{txtCep.Text}/json"))
                    {
                        if (!response.IsSuccessStatusCode)
                            throw new InvalidOperationException("Erro ao consultar o CEP!");

                        var resultado = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrWhiteSpace(resultado))
                            await DisplayAlert("Vish...", resultado, "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta!", ex.Message, "OK");
            }
        }
    }
}
