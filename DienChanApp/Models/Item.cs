using System;

namespace DienChanApp
{
    public class Item
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public string category { get; set; }
        public DateTime updateDate { get; set; }
        public int productId { get; set; }
    }
}
