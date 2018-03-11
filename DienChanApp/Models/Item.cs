using System;
using DienChanApp.Models;

namespace DienChanApp
{
    public class Item: Product
    {
        public int itemId { get; set; }
        public int quantity { get; set; }
        public DateTime updateDate { get; set; }
    }
}
