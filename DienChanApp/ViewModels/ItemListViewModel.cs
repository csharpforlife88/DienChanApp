using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DienChanApp.Views;
using Xamarin.Forms;
using System.Linq;

namespace DienChanApp.ViewModels
{
    public class ItemListViewModel: BaseViewModel
    {
        public ICommand AddItemCommand { get; private set; }
        public ICommand ChangeItemQuantityCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }

        public ItemListViewModel()
        {
            AddItemCommand = new Command(OnAddItem);
            ChangeItemQuantityCommand = new Command(OnChangeItemQuantity);
            RemoveItemCommand = new Command<ItemViewModel>(i => OnRemoveItem(i));
            //Items = new ObservableCollection<ItemViewModel>();
        }

        private static int _itemSelectedSub = 0;

        private async void OnAddItem()
        {
            if(_itemSelectedSub++ == 0)
                MessagingCenter.Subscribe<ProductListViewModel, ProductViewModel>(this, "ItemSelected", OnItemAdded);

            await Navigation.PushAsync(new ProductListView(IsAddNewItem: true));
        }

        private async void OnRemoveItem(ItemViewModel i)
        {
            if(Items.Count == 1)
            {
                await DisplayAlert("Warning", "You can't delete all items in the order", "OK");

                return;
            }

            if (!(await DisplayAlert("Confirmation", $"Are you sure to delete this item?", "Yes", "No"))) return;

            Items.Remove(i);

            MessagingCenter.Send(this, "ItemsChanged");
        }

        private void OnChangeItemQuantity()
        {
            MessagingCenter.Send(this, "ItemsChanged");
        }

        private void OnItemAdded(ProductListViewModel source,ProductViewModel product)
        {
            var item = Items.SingleOrDefault(i => i.ProductId == product.ProductId);

            if (item != null)
                item.Quantity++;
            else
                Items.Insert(0, new ItemViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Quantity = 1,
                    Price = product.Price,
                    //Category = product.Category,
                    UpdateDate = DateTime.Now,
                    ProductId = product.ProductId,
                    ImageUrl = product.ImageUrl
                });

            MessagingCenter.Send(this, "ItemsChanged");
        }

        private ObservableCollection<ItemViewModel> _items;
        public ObservableCollection<ItemViewModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
    }
}
