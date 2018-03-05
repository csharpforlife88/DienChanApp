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

        public static async Task<UserViewModel> AuthenticateUser(string username, string password)
        {
            var content = await _client.GetStringAsync(Url);

            var users = JsonConvert.DeserializeObject<List<User>>(Url);

            return MapService.MapUserModelToViewModel(users?.SingleOrDefault());
        }

        //Authenticate user
        //Register new user

        //Get All Orders
        //Create new order
        //Remove order
        //Edit order
        //Resend order information

        //Get All Products
        //Create new product
        //Remove product
        //Edit product
    }
}
