using BuscaCep.ViewModels;
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

            BindingContext = new BuscaCepViewModel();
        }

        private async void BtnBuscarCep_Clicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                ((BuscaCepViewModel)BindingContext).BuscarCommand.Execute(null);
            });
            
        }
    }
}
