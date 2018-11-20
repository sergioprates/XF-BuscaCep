using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuscaCep.ViewModels
{
    class ViewModelBase : INotifyPropertyChanged
    {

        private bool _isBusy = false;
        public bool IsBusy { get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy { get => !_isBusy; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected Task PushAsync(Page page, bool animated = true) => App.Current.MainPage.Navigation.PushAsync(page, animated);

        protected Task PopAsync(bool animated = true) => App.Current.MainPage.Navigation.PopAsync(animated);
    }
}
