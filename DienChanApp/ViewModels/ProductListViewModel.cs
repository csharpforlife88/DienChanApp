using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

            RefreshProductList();
        }

        private async void RefreshProductList(ProductViewModel o = null)
        {
            _productHelper = MapService.ToViewModels(await _restService.GetProducts());

            Products = new ObservableCollection<ProductViewModel>(_productHelper);
        }

        private async void OnCreateProduct()
        {
            MessagingCenter.Subscribe<ProductViewModel>(this, "ProductListRefresh", RefreshProductList);

            await Navigation.PushAsync(new ProductView());
        }

        private async void OnShowProduct()
        {
            MessagingCenter.Subscribe<ProductViewModel>(this, "ProductListRefresh", RefreshProductList);

            await Navigation.PushAsync(new ProductView(SelectedProduct));

            SelectedProduct = null;
        }

        private async void OnAddItem()
        {
            MessagingCenter.Send(this, "ItemSelected", SelectedProduct);

            await Navigation.PopAsync();
        }

        private async void OnDeleteProduct(ProductViewModel p)
        {
            if (!(await DisplayAlert("Confirmation", $"Are you sure to delete Product {p.Name}?", "Yes", "No"))) return;

            await Task.Run(async () =>
            {
                IsBusy = true;

                var result = await _restService.DeleteProduct(p.ProductId);

                OnRefresh();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Warning", "Product delete failed!", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", "Product deleted successfully!", "OK");
                    }
                });

                IsBusy = false;
            });
        }

        private void OnRefresh()
        {
            IsRefreshing = true;

            RefreshProductList();

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
