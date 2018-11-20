using BuscaCep.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace BuscaCep.ViewModels
{
    sealed class CepsViewModel : ViewModelBase
    {
        public CepsViewModel()
            : base()
        { }

        public ObservableCollection<string> Ceps { get; private set; } = new ObservableCollection<string>();

        private Command _buscarCommand;

        public Command BuscarCommand => _buscarCommand ?? (_buscarCommand = new Command(async () => await BuscarCommandExecute()));

        async Task BuscarCommandExecute()
        {
            MessagingCenter.Subscribe<BuscaCepViewModel>(this, "ADICIONAR CEP", (sender) =>
            {
                if (this.Ceps.Any(x => x == sender.CEP) == false)
                    this.Ceps.Add(sender.CEP);

                MessagingCenter.Unsubscribe<BuscaCepViewModel>(this, "ADICIONAR CEP");
            });

            await base.PushAsync(new BuscaCepPage())
        }
    }
}
