using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DienChanApp.Models;
using DienChanApp.Services;
using DienChanApp.Views;
using Xamarin.Forms;

namespace DienChanApp.ViewModels
{
    public class UserViewModel: BaseViewModel
    {
        public ICommand LogoutCommand { get; private set; }
        public ICommand SignupCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }

        public UserViewModel()
        {
            LogoutCommand = new Command(OnLogout);
            SignupCommand = new Command(OnSignup);
            CancelCommand = new Command(OnCancel);
            UpdateCommand = new Command(OnUpdate);
        }

        private async void OnSignup()
        {
            if (!await IsValidated()) return;

            await Task.Run(async () =>
            {
                IsBusy = true;

                var result = await _restService.CreateUser(MapService.ToModel(this));

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Warning", "You can't create login at this time!", "OK");

                        return;
                    }

                });

                IsBusy = false;
            });


            await Navigation.PushAsync(new LoginView());
        }

        private async void OnCancel()
        {
            await Navigation.PushAsync(new LoginView());
        }

        public async void OnLogout()
        {
            if (!(await DisplayAlert("Confirmation", $"Are you sure to log out?", "Yes", "No"))) return;

            var existingPages = Navigation.NavigationStack;

            for (var i = 0; i < existingPages.Count; i++)
            {
                Navigation.RemovePage(existingPages[i]);
            }

            Settings.Clear();

            await Navigation.PushAsync(new LoginView());
        }

        public async void OnUpdate()
        {
            
        }

        private async Task<bool> IsValidated()
        {
            if(string.IsNullOrEmpty(UserName))
            {
                await DisplayAlert("Warning", $"User name is missing!", "OK");

                return false;
            }

            if (UserName.Length < 4)
            {
                await DisplayAlert("Warning", $"User name must be at least 4 characters !", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await DisplayAlert("Warning", $"Password is missing!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(RetypePassword))
            {
                await DisplayAlert("Warning", $"Retype password is missing!", "OK");

                return false;
            }

            if (Password != RetypePassword)
            {
                await DisplayAlert("Warning", $"Password doesn't match!", "OK");

                return false;
            }

            if(Password.Length < 6)
            {
                await DisplayAlert("Warning", $"Password must be at least 6 characters!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                await DisplayAlert("Warning", $"First name is missing!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await DisplayAlert("Warning", $"Last name is missing!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(Email))
            {
                await DisplayAlert("Warning", $"Email is missing!", "OK");

                return false;
            }

            if (!IsValidEmail(Email))
            {
                await DisplayAlert("Warning", $"Email is invalid!", "OK");

                return false;
            }

            return true;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private int _id;
        public int ID 
        { 
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _userName;
        public string UserName 
        { 
            get { return _userName; } 
            set {SetProperty(ref _userName, value);}
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _retypePassword;
        public string RetypePassword
        {
            get { return _retypePassword; }
            set { SetProperty(ref _retypePassword, value); }
        }

        private string _currentPassword;
        public string CurrentPassword
        {
            get { return _currentPassword; }
            set { SetProperty(ref _currentPassword, value); }
        }

        private int _permissionId;
        public int PermissionId
        {
            get { return _permissionId; }
            set { SetProperty(ref _permissionId, value); }
        }

        private UserPermission _permission;
        public UserPermission Permission
        {
            get { return _permission; }
            set { SetProperty(ref _permission, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _email;
        public string Email 
        { 
            get { return _email; } 
            set { SetProperty(ref _email, value); }
        }

        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { SetProperty(ref _createdDate, value); }
        }
    }
}
