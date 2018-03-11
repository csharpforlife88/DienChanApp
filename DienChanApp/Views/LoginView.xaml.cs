using System;
using System.Collections.Generic;
using DienChanApp.ViewModels;
using Xamarin.Forms;

namespace DienChanApp.Views
{
    public partial class LoginView : ContentPage
    {
        private readonly LoginViewModel _vm = new LoginViewModel();
        public LoginView()
        {
            InitializeComponent();

            BindingContext = _vm;
        }
    }
}
