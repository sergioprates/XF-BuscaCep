using BuscaCep.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace BuscaCep.ViewModels
{
    sealed class CepsViewModel : ViewModelBase
    {
        public CepsViewModel()
            : base()
        {
            MessagingCenter.Subscribe<BuscaCepViewModel>(this, "ADICIONAR CEP",
                (sender) =>
                {

                    if (this.Ceps.Any(x=> x == sender.CEP) == false)
                        this.Ceps.Add(sender.CEP);
                });
        }

        public ObservableCollection<string> Ceps { get; private set; } = new ObservableCollection<string>();

        private Command _buscarCommand;

        public Command BuscarCommand => _buscarCommand ?? (_buscarCommand = new Command(async () => await base.PushAsync(new BuscaCepPage())));
    }
}
