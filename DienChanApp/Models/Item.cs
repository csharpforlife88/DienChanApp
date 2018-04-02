using System;
using DienChanApp.Models;

namespace DienChanApp
{
    public class Item
    {
        public int itemId { get; set; }

        public int quantity { get; set; }

        public DateTime updateDate { get; set; }

        public int productId { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string categoryName { get; set; }

        public decimal unitPrice { get; set; }

        public decimal weight { get; set; }

        public string imageUrl { get; set; }
    }
}
