using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DienChanApp
{
    public class ItemsViewModel : BaseViewModel
    {
        private int _itemId;
        public int ItemId
        {
            get { return _itemId; }
            set{SetProperty(ref _itemId, value);}
        }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public string category { get; set; }
        public DateTime updateDate { get; set; }
        public int productId { get; set; }
    }
}
