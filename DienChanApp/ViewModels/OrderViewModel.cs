using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DienChanApp.Models;
using DienChanApp.Views;
using Xamarin.Forms;

namespace DienChanApp.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public ICommand NavigateToItemViewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public OrderViewModel()
        {
            NavigateToItemViewCommand = new Command(OnNavigateToItemView);
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);

            Items = new ObservableCollection<ItemViewModel>();
        }

        private static int _itemChangedSub = 0;

        private async void OnNavigateToItemView()
        {
            if (_itemChangedSub++ == 0)
            {
                MessagingCenter.Subscribe<ItemListViewModel>(this, "ItemsChanged", OrderValueChanged);
                MessagingCenter.Subscribe<ItemViewModel>(this, "ItemsChanged", OrderValueChanged);
            }

            await Navigation.PushAsync(new ItemListView(Items));
        }

        private async void OnSave()
        {
            if (!await IsValidated()) return;

            if (!(await DisplayAlert("Confirmation", $"Are you sure to save this order?", "Yes", "No"))) return;

            //call api to update order

            MessagingCenter.Send(this, "OrderListRefresh");

            await Navigation.PopAsync();

        }

        private async void OnCancel()
        {
            if (!(await DisplayAlert("Confirmation", $"Are you sure to cancel this order?", "Yes", "No"))) return;

            MessagingCenter.Send(this, "OrderListRefresh");

            await Navigation.PopAsync();
        }

        private async Task<bool> IsValidated()
        {
            if(Customer.FirstName.Length == 0)
            {
                await DisplayAlert("Confirmation", $"First Name is missing!", "OK"); 

                return false;
            }

            if (Customer.LastName.Length == 0)
            {
                await DisplayAlert("Confirmation", $"Last Name is missing!", "OK");

                return false;
            }

            if (Customer.Address1.Length == 0)
            {
                await DisplayAlert("Confirmation", $"Address is missing!", "OK");

                return false;
            }

            if (Customer.City.Length == 0)
            {
                await DisplayAlert("Confirmation", $"City is missing!", "OK");

                return false;
            }

            if (Customer.Email.Length == 0)
            {
                await DisplayAlert("Confirmation", $"Email is missing!", "OK");

                return false;
            }

            if (Customer.Country.Length == 0)
            {
                await DisplayAlert("Confirmation", $"Country is missing!", "OK");

                return false;
            }

            if (Customer.Phone.Length == 0)
            {
                await DisplayAlert("Confirmation", $"Phone is missing", "OK");

                return false;
            }

            return true;
                
        }

        private int? _orderId;
        public int? OrderId
        {
            get { return _orderId; }
            set { SetProperty(ref _orderId, value); }
        }

        private CustomerViewModel _customer;
        public CustomerViewModel Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        private DateTime? _orderDate;
        public DateTime? OrderDate
        {
            get { return _orderDate; }
            set { SetProperty(ref _orderDate, value); }
        }

        private DateTime _lastUpdate;
        public DateTime LastUpdate
        {
            get { return _lastUpdate; }
            set { SetProperty(ref _lastUpdate, value); }
        }

        private ObservableCollection<ItemViewModel> _items;
        public ObservableCollection<ItemViewModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private decimal _subTotal;
        public decimal SubTotal
        {
            get { return _subTotal; }
            set { SetProperty(ref _subTotal, value); }
        }

        private decimal _tax;
        public decimal Tax
        {
            get { return _tax; }
            set
            {
                SetProperty(ref _tax, value);
                //OrderValueChanged();
            }
        }

        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set
            {
                SetProperty(ref _discount, value);
                OrderValueChanged();
            }
        }

        private decimal _orderTotal;
        public decimal OrderTotal
        {
            get { return _orderTotal; }
            set { SetProperty(ref _orderTotal, value); }
        }

        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
            set { SetProperty(ref _totalItems, value); }
        }

        protected void OrderValueChanged(ItemListViewModel sender = null)
        {
            TotalItems = Items.Sum(i => i.Quantity);

            SubTotal = Items.Sum(i => i.Quantity * i.Price);

            OrderTotal = SubTotal + Tax - Discount;
        }

        protected void OrderValueChanged(ItemViewModel sender)
        {
            OrderValueChanged();
        }
    }
}
