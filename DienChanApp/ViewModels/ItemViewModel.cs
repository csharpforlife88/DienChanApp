using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DienChanApp
{
    public class ItemViewModel : BaseViewModel
    {
        private int _itemId;
        public int ItemId
        {
            get { return _itemId; }
            set { SetProperty(ref _itemId, value); }
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

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { 
                SetProperty(ref _quantity, value);
                MessagingCenter.Send(this, "ItemsChanged");
            }
        }

        private decimal _price;
        public decimal Price{
            get { return _price; }
            set{SetProperty(ref _price, value);}
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set{SetProperty(ref _category, value);}
        }

        private DateTime _updateDate;
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set{SetProperty(ref _updateDate,value);}
        }

        private int _productId;
        public int ProductId
        {
            get { return _productId; }
            set{SetProperty(ref _productId, value);}
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }
    }
}
