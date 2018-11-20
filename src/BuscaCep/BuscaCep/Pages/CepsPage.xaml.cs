using BuscaCep.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuscaCep.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CepsPage : ContentPage
    {
        private bool _firstRun = true;

        public CepsPage()
        {
            InitializeComponent();

            base.BindingContext = new CepsViewModel();
        }

        

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            if (_firstRun)
            {
                ((CepsViewModel)BindingContext).RefreshCommand.Execute(null);
                _firstRun = false;
            }
                

            base.OnAppearing();
        }
    }
}
