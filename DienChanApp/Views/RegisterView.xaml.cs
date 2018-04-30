using System;
using System.Collections.Generic;
using DienChanApp.ViewModels;
using Xamarin.Forms;

namespace DienChanApp.Views
{
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            BindingContext = new UserViewModel();

            InitializeComponent();
        }
    }
}
