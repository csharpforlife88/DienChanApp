using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DienChanApp.Views;
using Xamarin.Forms;

namespace DienChanApp.ViewModels
{
    public class ItemListViewModel: BaseViewModel
    {
        public ICommand AddItemCommand { get; private set; }

        public ItemListViewModel()
        {
            AddItemCommand = new Command(OnAddItem);

            _items = new ObservableCollection<ItemViewModel>();
        }

        private async void OnAddItem()
        {
            await Navigation.PushAsync(new ProductListView(IsAddNewItem: true));
        }

        private ObservableCollection<ItemViewModel> _items;
        public ObservableCollection<ItemViewModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
    }
}
