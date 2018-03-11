using System;
using System.Collections.Generic;
using DienChanApp.ViewModels;
using Xamarin.Forms;

namespace DienChanApp.Views
{
    public partial class ProductView : ContentPage
    {
        public ProductView(ProductViewModel vm = null)
        {
            BindingContext = vm;

            InitializeComponent();
        }
    }
}
