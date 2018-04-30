using System;
using System.Windows.Input;
using DienChanApp.Models;
using Xamarin.Forms;
using Plugin.Media;
using System.Threading.Tasks;
using DienChanApp.Services;

namespace DienChanApp.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand ChooseImageCommand { get; set; }

        public ProductViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            ChooseImageCommand = new Command(OnChooseImage);
        }

        private async void OnSave()
        {
            if (!await IsValidated()) return;

            if (!(await DisplayAlert("Confirmation", $"Are you sure to save this product?", "Yes", "No"))) return;

            //call api to update order

            await Task.Run(async () =>
            {
                IsBusy = true;

                var result = ProductId == 0
                    ? (await _restService.CreateProduct(MapService.ToModel(this)))
                    : (await _restService.UpdateProduct(MapService.ToModel(this)));

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Warning", "Product save failed!", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", "Product save successfully!", "OK");
                    }
                });

                IsBusy = false;
            });


            MessagingCenter.Send(this, "ProductListRefresh");

            await Navigation.PopAsync();

        }

        private async void OnCancel()
        {
            if (!(await DisplayAlert("Confirmation", $"Are you sure to cancel this product change?", "Yes", "No"))) return;

            MessagingCenter.Send(this, "OrderListRefresh");

            await Navigation.PopAsync();
        }

        private async void OnChooseImage()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }

            using (var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            }))
            {
                if (file == null)
                    return;

                using(var fs = new System.IO.FileStream(file.Path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    var imageData = new byte[fs.Length];

                    fs.Read(imageData, 0, Convert.ToInt32(fs.Length));

                    Image = Convert.ToBase64String(imageData);
                }

                ImageUrl = file.Path;

                IsImageUpdate = true;
            }
        }

        private async Task<bool> IsValidated()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await DisplayAlert("Warning", $"Product Name is missing!", "OK");

                return false;
            }

            if (string.IsNullOrEmpty(Description))
            {
                await DisplayAlert("Warning", $"Product Description is missing!", "OK");

                return false;
            }

            if (Price == 0m)
            {
                await DisplayAlert("Warning", $"Product Price can't be 0!", "OK");

                return false;
            }

            //if (Category.Length == 0)
            //{
            //    await DisplayAlert("Confirmation", $"First Name is missing!", "OK");

            //    return false;
            //}

            return true;

        }

        private int _productId;
        public int ProductId
        {
            get { return _productId; }
            set { SetProperty(ref _productId, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private decimal _weight;
        public decimal Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }

        private int _categoryId;
        public int CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        private Category _category;
        public Category Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private bool _isImageUpdate;
        public bool IsImageUpdate
        {
            get { return _isImageUpdate; }
            set { SetProperty(ref _isImageUpdate, value); }
        }
    }
}
