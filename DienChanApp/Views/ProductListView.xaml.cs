using System;
using System.Collections.Generic;
using DienChanApp.ViewModels;
using Xamarin.Forms;

namespace DienChanApp.Views
{
    public partial class ProductListView : ContentPage
    {
        private readonly ProductListViewModel _vm = new ProductListViewModel();

        public ProductListView(bool IsAddNewItem = false)
        {
            _vm.IsAddNewItem = IsAddNewItem;

            BindingContext = _vm;

            InitializeComponent();
        }

        public ProductListView()
        {
            BindingContext = _vm;

            InitializeComponent();
        }
    }
}
