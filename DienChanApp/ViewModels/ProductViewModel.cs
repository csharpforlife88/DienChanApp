using System;
using DienChanApp.Models;

namespace DienChanApp.ViewModels
{
    public class ProductViewModel: BaseViewModel
    {
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
            set { SetProperty(ref _isImageUpdate, value);}
        }
    }
}
