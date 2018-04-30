using System;
using DienChanApp.Models;
using DienChanApp.ViewModels;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
namespace DienChanApp.Services
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(nameof(Token), string.Empty);

            set => AppSettings.AddOrUpdateValue(nameof(Token), value);

        }

        public static UserViewModel User { get; set; }

        public static int UserId
        {
            get => AppSettings.GetValueOrDefault(nameof(UserId), 0);

            set => AppSettings.AddOrUpdateValue(nameof(UserId), value);

        }

        public static void Clear()
        {
            User = null;
            Token = null;
            UserId = 0;
        }



    }
}
