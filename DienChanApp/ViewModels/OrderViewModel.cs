using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DienChanApp.Models;
using DienChanApp.Services;
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
            Customer = new CustomerViewModel();
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

            await Task.Run(async () =>
            {
                IsBusy = true;

                var result = OrderId == 0 
                    ? (await _restService.CreateOrder(MapService.ToModel(this)))
                    :(await _restService.UpdateOrder(MapService.ToModel(this)));

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Warning", "Order save failed!", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", "Order save successfully!", "OK");
                    }
                });

                IsBusy = false;
            });


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
            if(string.IsNullOrEmpty(Customer.FirstName))
            {
                await DisplayAlert("Warning", $"First Name is missing!", "OK"); 

                return false;
            }

            if (string.IsNullOrEmpty(Customer.LastName))
            {
                await DisplayAlert("Warning", $"Last Name is missing!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(Customer.Address1))
            {
                await DisplayAlert("Warning", $"Address is missing!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(Customer.City))
            {
                await DisplayAlert("Warning", $"City is missing!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(Customer.Email))
            {
                await DisplayAlert("Warning", $"Email is missing!", "OK");

                return false;
            }

            if(!IsValidEmail(Customer.Email))
            {
                await DisplayAlert("Warning", $"Email is invalid!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(Customer.Country))
            {
                await DisplayAlert("Warning", $"Country is missing!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(Customer.Phone))
            {
                await DisplayAlert("Warning", $"Phone is missing!", "OK");

                return false;
            }

            if(Items.Count == 0)
            {
                await DisplayAlert("Warning", $"You need to at least add an item!", "OK");

                return false;
            }

            return true;
                
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private int _orderId;
        public int OrderId
        {
            get { return _orderId; }
            set { SetProperty(ref _orderId, value); }
        }

        private int _customerId;
        public int CustomerId
        {
            get { return _customerId; }
            set { SetProperty(ref _customerId, value); }
        }

        private CustomerViewModel _customer;
        public CustomerViewModel Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        private DateTime _orderDate;
        public DateTime OrderDate
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
                OrderValueChanged();
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

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set { SetProperty(ref _active, value); }
        }

        private DateTime _updateDate;
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set { SetProperty(ref _updateDate, value); }
        }

        private bool _isItemUpdate;
        public bool IsItemUpdate
        {
            get { return _isItemUpdate; }
            set { SetProperty(ref _isItemUpdate, value); }
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
