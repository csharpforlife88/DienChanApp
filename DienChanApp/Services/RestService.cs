using System;
using System.Collections.Generic;
using System.Net.Http;
using DienChanApp.Models;
using DienChanApp.ViewModels;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DienChanApp.Services
{
    public static class RestService
    {
        private const string Url = "https://jsonplaceholder.typicode.com/users";
        private static HttpClient _client = new HttpClient();

        public static async Task<User> AuthenticateUser(string username, string password)
        {
            var content = await _client.GetStringAsync(Url);

            var users = JsonConvert.DeserializeObject<List<User>>(Url);

            return users?.SingleOrDefault();
        }

        public static List<Product> GetProducts()
        {
            return MockRestService.GetProducts();
        }

        public static List<Order> GetOrders()
        {
            return MockRestService.GetOrders();
        }


        //Authenticate user
        //Register new user

        //Get All Orders
        //Create new Order
        //Remove Order
        //Edit Order
        //Resend Order information

        //Get All Orders
        //Create new Order
        //Remove Order
        //Edit Order
    }
}
