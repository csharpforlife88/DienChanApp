using System;
namespace DienChanApp.Models
{
    public class Product
    {
        public int productId { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }

        public decimal weight { get; set; }

        public int categoryId { get; set; }

        public Category category { get; set; }

        public string imageUrl { get; set; }

        public string image { get; set; }

        public bool isImageUpdate { get; set; }
    }
}
