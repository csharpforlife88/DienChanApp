using System;
using System.Collections.Generic;
using DienChanApp.ViewModels;
using Xamarin.Forms;

namespace DienChanApp.Views
{
    public partial class OrderListView : ContentPage
    {
        private readonly OrderListViewModel _vm = new OrderListViewModel();

        public OrderListView()
        {
            BindingContext = _vm;

            InitializeComponent();
        }
    }
}
