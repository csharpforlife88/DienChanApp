using System;
using System.Collections.Generic;
using DienChanApp.ViewModels;
using Xamarin.Forms;

namespace DienChanApp.Views
{
    public partial class OrderView : ContentPage
    {
        public OrderView(OrderViewModel vm = null)
        {
            BindingContext = vm ?? new OrderViewModel();

            InitializeComponent();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            
        }
    }
}
