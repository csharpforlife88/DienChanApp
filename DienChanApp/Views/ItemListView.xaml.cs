using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DienChanApp.ViewModels;
using Xamarin.Forms;

namespace DienChanApp.Views
{
    public partial class ItemListView : ContentPage
    {
        private readonly ItemListViewModel _vm = new ItemListViewModel();

        public ItemListView(ObservableCollection<ItemViewModel> items = null)
        {
            _vm.Items = items;

            BindingContext = _vm;

            InitializeComponent();
        }
    }
}
