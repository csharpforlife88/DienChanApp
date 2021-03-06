﻿using System;
using DienChanApp.Views;
using Xamarin.Forms;

namespace DienChanApp
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new NavigationPage(new LoginView());
            else
                MainPage = new NavigationPage(new LoginView());
        }
    }
}
