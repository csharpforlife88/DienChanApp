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
                username = vm.UserName,
                password = vm.Password,
                permissionId = vm.PermissionId,
                permission = vm.Permission,
                firstName = vm.FirstName,
                lastName = vm.LastName,
                email = vm.Email,
                createdDate = vm.CreatedDate
            };
        }

        public static UserViewModel ToViewModel(User m)
        {
            return new UserViewModel
            {
                ID = m.id,
                UserName = m.username,
                Password = m.password,
                PermissionId = m.permissionId,
                Permission = m.permission,
                FirstName = m.firstName,
                LastName = m.lastName,
                Email = m.email,
                CreatedDate = m.createdDate
            };
        }

        public static Item ToModel(ItemViewModel vm)
        {
            return new Item
            {
                itemId = vm.ItemId,
                quantity = vm.Quantity,
                updateDate = vm.UpdateDate,
                productId = vm.ProductId,
                name = vm.Name,
                categoryName = vm.CategoryName,
                description = vm.Description,
                unitPrice = vm.Price,
                weight = vm.Weight,
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
                Quantity = m.quantity,
                UpdateDate = m.updateDate,
                ProductId = m.productId,
                Name = m.name,
                CategoryName = m.categoryName,
                Description = m.description,
                Price = m.unitPrice,
                Weight = m.weight,
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
                phoneNumber = vm.Phone,
                address1 = vm.Address1,
                address2 = vm.Address2,
                city = vm.City,
                state = vm.State,
                zip = vm.Zip,
                country = vm.Country,
                updateDate = vm.UpdateDate
            };
        }

        public static CustomerViewModel ToViewModel(Customer m){
            return new CustomerViewModel
            {
                CustomerId = m.customerId,
                FirstName = m.firstName,
                LastName = m.lastName,
                Email = m.email,
                Phone = m.phoneNumber,
                Address1 = m.address1,
                Address2 = m.address2,
                City = m.city,
                State = m.state,
                Zip = m.zip,
                Country = m.country,
                UpdateDate = m.updateDate
            };
        }

        public static Order ToModel(OrderViewModel vm){
            return new Order
            {
                orderId = vm.OrderId,
                customerId = vm.CustomerId,
                customer = ToModel(vm.Customer),
                orderDate = vm.OrderDate,
                lastUpdate = vm.LastUpdate,
                items = ToModels(new List<ItemViewModel>(vm.Items)),
                subTotal = vm.SubTotal,
                tax = vm.Tax,
                discount = vm.Discount,
                orderTotal = vm.OrderTotal,
                active = vm.Active,
                updateDate = vm.UpdateDate,
                isItemUpdate = vm.IsItemUpdate
            };
        }

        public static OrderViewModel ToViewModel(Order m){
            return new OrderViewModel
            {
                OrderId = m.orderId,
                CustomerId = m.customerId,
                Customer = ToViewModel(m.customer),
                OrderDate = m.orderDate,
                LastUpdate = m.lastUpdate,
                Items = new ObservableCollection<ItemViewModel>(ToViewModels(m.items)),
                SubTotal = m.subTotal,
                Tax = m.tax,
                Discount = m.discount,
                OrderTotal = m.orderTotal,
                Active = m.active,
                UpdateDate = m.updateDate
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
                categoryId = vm.CategoryId,
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
                CategoryId = m.categoryId,
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
