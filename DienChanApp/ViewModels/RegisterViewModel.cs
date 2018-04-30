using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DienChanApp.Services;
using DienChanApp.Views;
using Xamarin.Forms;

namespace DienChanApp.ViewModels
{
    public class RegisterViewModel:BaseViewModel
    {
        public ICommand SignupCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public RegisterViewModel()
        {
            SignupCommand = new Command(OnSignup);
            CancelCommand = new Command(OnCancel);
        }

        private async void OnSignup()
        {
            if (!await IsValidated()) return;

            await Task.Run(async () =>
            {
                IsBusy = true;

                var result = await _restService.CreateUser(MapService.ToModel(User));

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Warning", "Order save failed!", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", "Order save successfully!", "OK");
                    }
                });

                IsBusy = false;
            });


            MessagingCenter.Send(this, "OrderListRefresh");

            await Navigation.PopAsync();
        }

        private async void OnCancel()
        {
            await Navigation.PushAsync(new LoginView());
        }

        private async Task<bool> IsValidated()
        {
            return true;
        }

        private UserViewModel _user;
        public UserViewModel User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
    }
}




