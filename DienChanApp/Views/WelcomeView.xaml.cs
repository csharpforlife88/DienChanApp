using System;
using System.Collections.Generic;
using DienChanApp.ViewModels;
using Xamarin.Forms;

namespace DienChanApp.Views
{
    public partial class WelcomeView : ContentPage
    {
        private readonly WelcomeViewModel _vm;
        public WelcomeView()
        {
            InitializeComponent();

            _vm = new WelcomeViewModel();

            _vm.Navigation = Navigation;

            BindingContext = _vm;
        }
    }
}
