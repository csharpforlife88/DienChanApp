using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DienChanApp.Models;
using DienChanApp.Views;
using Xamarin.Forms;

namespace DienChanApp.ViewModels
{
    public class OrderViewModel: BaseViewModel
    {
        public ICommand NavigateToItemViewCommand { get; private set; }
        public OrderViewModel()
        {
            NavigateToItemViewCommand = new Command<ObservableCollection<ItemViewModel>>(vms => OnNavigateToItemView(vms));
            Items = new ObservableCollection<ItemViewModel>();
        }

        private async void OnNavigateToItemView(ObservableCollection<ItemViewModel> vms)
        {
            await Navigation.PushAsync(new ItemListView(vms));
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

        public ObservableCollection<ItemViewModel> Items { get; set; }

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
            set { SetProperty(ref _tax, value); }
        }

        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set { SetProperty(ref _discount, value); }
        }

        private decimal _orderTotal;
        public decimal OrderTotal
        {
            get { return _orderTotal; }
            set{SetProperty(ref _orderTotal, value);}
        }
    }
}
