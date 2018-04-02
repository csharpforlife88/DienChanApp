using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DienChanApp.Services;
using DienChanApp.Views;
using Xamarin.Forms;

namespace DienChanApp.ViewModels
{
    public class OrderListViewModel : BaseViewModel
    {
        public ICommand CreateOrderCommand { get; private set; }
        public ICommand DeleteOrderCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        private List<OrderViewModel> _orderHelper;
        private static readonly RestService _restService = new RestService();


        public OrderListViewModel()
        {
            CreateOrderCommand = new Command(OnCreateOrder);
            DeleteOrderCommand = new Command<OrderViewModel>( o => OnDeleteOrder(o));
            RefreshCommand = new Command(OnRefresh);

            RefreshOrderList();
        }

        private async void OnCreateOrder()
        {
            MessagingCenter.Subscribe<OrderViewModel>(this, "OrderListRefresh", RefreshOrderList);

            await Navigation.PushAsync(new OrderView());
        }

        private async void OnShowOrder()
        {
            MessagingCenter.Subscribe<OrderViewModel>(this, "OrderListRefresh", RefreshOrderList);

            await Navigation.PushAsync(new OrderView(SelectedOrder));

            SelectedOrder = null;
        }

        private async void RefreshOrderList(OrderViewModel o = null)
        {
            _orderHelper = MapService.ToViewModels(await _restService.GetOrders());

            Orders = new ObservableCollection<OrderViewModel>(_orderHelper);
        }

        private async void OnDeleteOrder(OrderViewModel o)
        {
            if (!(await DisplayAlert("Confirmation", $"Are you sure to delete order {o.OrderId}?", "Yes", "No"))) return;

            var result = await _restService.DeleteOrder(o.OrderId);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                OnRefresh();

                await DisplayAlert("Warning", "Order delete failed!", "OK");
            }
            else
            {
                _orderHelper.Remove(o);

                OnSearchOrder();

                await DisplayAlert("Confirmation", "Order deleted successfully!", "OK");
            }
        }

        private void OnRefresh()
        {
            IsRefreshing = true;

            RefreshOrderList();

            OnSearchOrder();

            IsRefreshing = false;
        }

        private void OnSearchOrder()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                Orders = new ObservableCollection<OrderViewModel>(_orderHelper);
            else
                Orders = new ObservableCollection<OrderViewModel>(_orderHelper.Where(o =>
                            o.Customer.FullName.ToLower().Contains(SearchText.ToLower())
                            || o.OrderId.ToString().StartsWith(SearchText, StringComparison.InvariantCultureIgnoreCase)));
        }

        private ObservableCollection<OrderViewModel> _orders;
        public ObservableCollection<OrderViewModel> Orders
        {
            get { return _orders; }
            set { SetProperty(ref _orders, value); }
        }

        private OrderViewModel _selectedOrder;
        public OrderViewModel SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                SetProperty(ref _selectedOrder, value);

                if (value != null)
                    OnShowOrder();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);
                OnSearchOrder();
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
    }
}
