using System;
using System.Threading.Tasks;
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

        public void OnLogin()
        {
            if(string.IsNullOrEmpty(UserName))
            {
                DisplayAlert("Warning", "Please enter your username!", "OK");

                return;
            }

            if(string.IsNullOrEmpty(Password))
            {
                DisplayAlert("Warning", "Please enter your password!", "OK");

                return;
            }

            Task.Run(async () =>
            {
                IsBusy = true;

                var user = await _restService.AuthenticateUser(UserName, Password);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (user != null && user.username == UserName)
                        await Navigation.PushAsync(new MainView());
                    else
                    {
                        await DisplayAlert("Warning", "User name or password incorrect!", "OK");

                        UserName = "";
                        Password = "";
                    }
                });

                IsBusy = false;
            });
        }

        public async void OnRegister()
        {
            await Navigation.PushAsync(new RegisterView());
        }
    }
}
