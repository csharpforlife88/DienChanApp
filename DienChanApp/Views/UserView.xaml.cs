using System;
using System.Collections.Generic;
using DienChanApp.Services;
using DienChanApp.ViewModels;
using Xamarin.Forms;

namespace DienChanApp.Views
{
    public partial class UserView : ContentPage
    {
        public UserView()
        {
            InitializeComponent();

            BindingContext = Settings.User;
        }
    }
}
