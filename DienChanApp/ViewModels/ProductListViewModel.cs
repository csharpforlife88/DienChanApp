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
    public class ProductListViewModel: BaseViewModel
    {
        public ICommand CreateProductCommand { get; private set; }
        public ICommand DeleteProductCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        private List<ProductViewModel> _productHelper;


        public ProductListViewModel()
        {
            CreateProductCommand = new Command(OnCreateProduct);
            DeleteProductCommand = new Command<ProductViewModel>(o => OnDeleteProduct(o));
            RefreshCommand = new Command(OnRefresh);

            Products = new ObservableCollection<ProductViewModel>();

            _productHelper = MapService.ToViewModels(RestService.GetProducts());

            Products = new ObservableCollection<ProductViewModel>(_productHelper);
        }

        private async void OnCreateProduct()
        {
            await Navigation.PushAsync(new ProductView());
        }

        private async void OnShowProduct()
        {
            await Navigation.PushAsync(new ProductView(SelectedProduct));

            SelectedProduct = null;
        }

        private async void OnAddItem()
        {
            MessagingCenter.Send(this, "ItemSelected", SelectedProduct);

            await Navigation.PopAsync();
        }

        private async void OnDeleteProduct(ProductViewModel o)
        {
            if (!(await DisplayAlert("Confirmation", $"Are you sure to delete Product {o.ProductId}?", "Yes", "No"))) return;

            _productHelper.Remove(o);

            OnSearchProduct();
        }

        private void OnRefresh()
        {
            IsRefreshing = true;

            OnSearchProduct();

            IsRefreshing = false;
        }

        private void OnSearchProduct()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                Products = new ObservableCollection<ProductViewModel>(_productHelper);
            else
                Products = new ObservableCollection<ProductViewModel>(_productHelper.Where(o =>
                            o.Name.ToLower().Contains(SearchText.ToLower())
                            || o.ProductId.ToString().StartsWith(SearchText, StringComparison.InvariantCultureIgnoreCase)));
        }

        private ObservableCollection<ProductViewModel> _Products;
        public ObservableCollection<ProductViewModel> Products
        {
            get { return _Products; }
            set { SetProperty(ref _Products, value); }
        }

        private ProductViewModel _selectedProduct;
        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                SetProperty(ref _selectedProduct, value);

                if (value == null) return;

                if (IsAddNewItem)
                    OnAddItem();
                else
                    OnShowProduct();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);
                OnSearchProduct();
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        private bool _isAddNewItem;
        public bool IsAddNewItem
        {
            get { return _isAddNewItem; }
            set { SetProperty(ref _isAddNewItem, value); }
        }
    }
}
