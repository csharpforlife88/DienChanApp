using System;
using System.Collections.Generic;
using DienChanApp.Models;

namespace DienChanApp.Services
{
    public static class MockRestService
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    productId = 1,
                    name = "Item Name 1",
                    description = "Product 1 Description",
                    category = "tool",
                    price = 10,
                    weight = 100,
                    imageUrl = "https://picsum.photos/200/200"
                },
                new Product
                {
                    productId = 2,
                    name = "Item Name 2",
                    description = "Product 2 Description",
                    category = "tool",
                    price = 20,
                    weight = 200,
                    imageUrl = "https://picsum.photos/200/200"
                },
                new Product
                {
                    productId = 3,
                    name = "Item Name 3",
                    description = "Product 3 Description",
                    category = "tool",
                    price = 30,
                    weight = 300,
                    imageUrl = "https://picsum.photos/200/200"
                }
            };
        }

        public static List<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order{
                    orderId = 20180001,
                    customer = new Customer{
                        customerId = 1,
                        firstName = "Tu",
                        lastName = "Nguyen",
                        email = "tunguyen161088@gmail.com",
                        phone="631-977-0728",
                        address1 = "34 Clifton Place",
                        city = "Port Jefferson Station",
                        zip = "11776",
                        state = "New York",
                        country = "United States"
                    },
                    orderDate = DateTime.Now,
                    lastUpdate = DateTime.Now,
                    items = new List<Item>{
                        new Item
                        {
                            itemId = 1,
                            name = "Item Name 1",
                            description = "Item Description 1",
                            quantity = 2,
                            price = 10,
                            weight = 100,
                            category = "Tool",
                            updateDate = DateTime.Now,
                            productId = 1,
                            imageUrl = "https://picsum.photos/200/200"
                        },
                        new Item
                        {
                            itemId = 2,
                            name = "Item Name 2",
                            description = "Item Description 2",
                            quantity = 2,
                            price = 20,
                            weight = 200,
                            category = "Tool",
                            updateDate = DateTime.Now,
                            productId = 2,
                            imageUrl = "https://picsum.photos/200/200"
                        },
                        //new Item
                        //{
                        //    itemId = 3,
                        //    name = "Item Name 3",
                        //    description = "Item Description 3",
                        //    quantity = 2,
                        //    price = 30,
                        //    weight = 300,
                        //    category = "Tool",
                        //    updateDate = DateTime.Now,
                        //    productId = 3,
                        //    imageUrl = "https://picsum.photos/200/200"
                        //}
                    },
                    subTotal = 100m,
                    tax = 10m,
                    discount = 20m,
                    orderTotal = 90m
                },
                new Order{
                    orderId = 20180004,
                    customer = new Customer{
                        customerId = 1,
                        firstName = "Tina",
                        lastName = "Huynh",
                        email = "tina110189@gmail.com",
                        phone="2396878479",
                        address1 = "704 sw 16th ave",
                        address2 = "apt 301",
                        city = "Gainesville",
                        zip = "32601",
                        state = "Florida",
                        country = "United States"
                    },
                    orderDate = DateTime.Now,
                    lastUpdate = DateTime.Now,
                    items = new List<Item>{
                        new Item
                        {
                            itemId = 1,
                            name = "Item Name 2",
                            description = "Item Description 2",
                            quantity = 2,
                            price = 15,
                            category = "Tool",
                            updateDate = DateTime.Now,
                            productId = 2
                        }
                    },
                    subTotal = 100m,
                    tax = 10m,
                    discount = 20m,
                    orderTotal = 90m
                },
                new Order{
                    orderId = 20180007,
                    customer = new Customer{
                        customerId = 1,
                        firstName = "Sophia",
                        lastName = "Huynh",
                        email = "xohuynh@gmail.com",
                        phone="631-977-0715",
                        address1 = "555 Oxford ave",
                        city = "Tampa",
                        zip = "33417",
                        state="Florida",
                        country = "United States"
                    },
                    orderDate = DateTime.Now,
                    lastUpdate = DateTime.Now,
                    items = new List<Item>{
                        new Item
                        {
                            itemId = 1,
                            name = "Item Name 1",
                            description = "Item Description 1",
                            quantity = 2,
                            price = 15,
                            category = "Tool",
                            updateDate = DateTime.Now,
                            productId = 2
                        }
                    },
                    subTotal = 100m,
                    tax = 10m,
                    discount = 20m,
                    orderTotal = 90m
                },

            };
        }
    }
}
