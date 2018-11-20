using BuscaCep.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using BuscaCep.Cross;
using BuscaCep.Data.Entidades;

namespace BuscaCep.ViewModels
{
    sealed class CepsViewModel : ViewModelBase
    {
        public CepsViewModel()
            : base()
        { }

        public ObservableCollection<Cep> Ceps { get; private set; } = new ObservableCollection<Cep>();

        private Command _buscarCommand;

        public Command BuscarCommand => _buscarCommand ?? (_buscarCommand = new Command(async () => await BuscarCommandExecute()));

        private Command _refreshCommand;

        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(async () => await RefreshCommandExecute(), () => IsNotBusy));

        async Task RefreshCommandExecute()
        {

            if (this.IsBusy)
                return;

            this.IsBusy = true;
            this.RefreshCommand.ChangeCanExecute();

            try
            {
                Ceps.Clear();

                foreach (var item in Data.DatabaseService.Current.Listar())
                {
                    this.Ceps.Add(item);
                }

                await Task.FromResult(0);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta!", ex.Message, "OK");
            }
            finally
            {
                this.IsBusy = false;
                this.RefreshCommand.ChangeCanExecute();
            }
           
        }

        async Task BuscarCommandExecute()
        {
            MessagingCenter.Subscribe<BuscaCepViewModel>(this, MessageKeys.CepsAtualizados, (sender) =>
            {
                this.RefreshCommand.Execute(null);

                MessagingCenter.Unsubscribe<BuscaCepViewModel>(this, MessageKeys.CepsAtualizados);
            });

            await base.PushAsync(new BuscaCepPage());
        }
    }
}
