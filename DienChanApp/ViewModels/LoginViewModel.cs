using System;
using System.Windows.Input;
using DienChanApp.Services;
using DienChanApp.Views;
using Xamarin.Forms;

namespace DienChanApp.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
        }

        private string _userName;
        public string UserName 
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public async void OnLogin()
        {
            var user = await RestService.AuthenticateUser(UserName, Password);

            if (user != null)
                await Navigation.PushAsync(new MainView());
            else
            {
                await DisplayAlert("Warning", "User name or password incorrect!", "OK");

                UserName = "";
                Password = "";
            }
        }

        public async void OnRegister()
        {
            return;

            await Navigation.PushAsync(new MainView());
        }
    }
}
