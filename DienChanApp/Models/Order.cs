using System;
using System.Collections.Generic;

namespace DienChanApp.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public Customer customer { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public List<Item> items { get; set; }
        public decimal subTotal { get; set; }
        public decimal tax { get; set; }
        public decimal discount { get; set; }
        public decimal orderTotal { get; set; }

    }
}
