using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DienChanApp.Models;
using DienChanApp.ViewModels;

namespace DienChanApp.Services
{
    public static class MapService
    {
        public static User ToModel(UserViewModel vm)
        {
            return new User
            {
                id = vm.ID,
                name = vm.Name,
                phone = vm.Phone,
                email = vm.Email,
                company = vm.Company
            };
        }

        public static UserViewModel ToViewModel(User m)
        {
            return new UserViewModel
            {
                ID = m.id,
                Name = m.name,
                Phone = m.phone,
                Email = m.email,
                Company = m.company
            };
        }

        public static Item ToModel(ItemViewModel vm)
        {
            return new Item
            {
                itemId = vm.ItemId,
                name = vm.Name,
                description = vm.Description,
                quantity = vm.Quantity,
                price = vm.Price,
                category = vm.Category,
                updateDate = vm.UpdateDate,
                productId = vm.ProductId,
                imageUrl = vm.ImageUrl
            };
        }

        public static List<Item> ToModels(List<ItemViewModel> vms)
        {
            var result = new List<Item>();

            vms.ForEach(vm => result.Add(ToModel(vm)));

            return result;
        }

        public static ItemViewModel ToViewModel(Item m){
            return new ItemViewModel
            {
                ItemId = m.itemId,
                Name = m.name,
                Description = m.description,
                Quantity = m.quantity,
                Price = m.price,
                Category = m.category,
                UpdateDate = m.updateDate,
                ProductId = m.productId,
                ImageUrl = m.imageUrl
            };
        }

        public static List<ItemViewModel> ToViewModels(List<Item> ms)
        {
            var result = new List<ItemViewModel>();

            ms.ForEach(m => result.Add(ToViewModel(m)));

            return result;
        }

        public static Customer ToModel(CustomerViewModel vm){
            return new Customer
            {
                customerId = vm.CustomerId,
                firstName = vm.FirstName,
                lastName = vm.LastName,
                email = vm.Email,
                phone = vm.Phone,
                address1 = vm.Address1,
                address2 = vm.Address2,
                city = vm.City,
                state = vm.State,
                zip = vm.Zip,
                country = vm.Country
            };
        }

        public static CustomerViewModel ToViewModel(Customer m){
            return new CustomerViewModel
            {
                CustomerId = m.customerId,
                FirstName = m.firstName,
                LastName = m.lastName,
                Email = m.email,
                Phone = m.phone,
                Address1 = m.address1,
                Address2 = m.address2,
                City = m.city,
                State = m.state,
                Zip = m.zip,
                Country = m.country
            };
        }

        public static Order ToModel(OrderViewModel vm){
            return new Order
            {
                orderId = vm.OrderId,
                customer = ToModel(vm.Customer),
                orderDate = vm.OrderDate,
                lastUpdate = vm.LastUpdate,
                items = ToModels(new List<ItemViewModel>(vm.Items)),
                subTotal = vm.SubTotal,
                tax = vm.Tax,
                discount = vm.Discount,
                orderTotal = vm.OrderTotal
            };
        }

        public static OrderViewModel ToViewModel(Order m){
            return new OrderViewModel
            {
                OrderId = m.orderId,
                Customer = ToViewModel(m.customer),
                OrderDate = m.orderDate,
                LastUpdate = m.lastUpdate,
                Items = new ObservableCollection<ItemViewModel>(ToViewModels(m.items)),
                SubTotal = m.subTotal,
                Tax = m.tax,
                Discount = m.discount,
                OrderTotal = m.orderTotal
            };
        }

        public static List<OrderViewModel> ToViewModels(List<Order> ms)
        {
            var result = new List<OrderViewModel>();

            ms.ForEach(m => result.Add(ToViewModel(m)));

            return result;
        }

        public static Product ToModel(ProductViewModel vm){
            return new Product
            {
                productId = vm.ProductId,
                name = vm.Name,
                description = vm.Description,
                price = vm.Price,
                weight = vm.Weight,
                category = vm.Category,
                imageUrl = vm.ImageUrl
            };
        }

        public static List<Product> ToModels(List<ProductViewModel> vms)
        {
            var result = new List<Product>();

            vms.ForEach(vm => result.Add(ToModel(vm)));

            return result;
        }

        public static ProductViewModel ToViewModel(Product m)
        {
            return new ProductViewModel
            {
                ProductId = m.productId,
                Name = m.name,
                Description = m.description,
                Price = m.price,
                Weight = m.weight,
                Category = m.category,
                ImageUrl = m.imageUrl
            };   
        }

        public static List<ProductViewModel> ToViewModels(List<Product> ms)
        {
            var result = new List<ProductViewModel>();

            ms.ForEach(m => result.Add(ToViewModel(m)));

            return result;
        }
    }
}
