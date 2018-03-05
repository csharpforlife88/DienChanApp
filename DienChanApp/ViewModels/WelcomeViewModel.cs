using System;
using System.Windows.Input;
using DienChanApp.Views;
using Xamarin.Forms;

namespace DienChanApp.ViewModels
{
    public class WelcomeViewModel: BaseViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        public WelcomeViewModel()
        {
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
        }

        public async void OnLogin()
        {
            await Navigation.PushAsync(new LoginView());
        }

        public async void OnRegister()
        {
            await Navigation.PushAsync(new LoginView());
        }
    }
}
