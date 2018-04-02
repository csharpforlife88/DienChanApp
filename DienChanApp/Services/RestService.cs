using System;
using System.Collections.Generic;
using System.Net.Http;
using DienChanApp.Models;
using DienChanApp.ViewModels;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace DienChanApp.Services
{
    public class RestService
    {
        private const string baseUrl = "http://api.dienchanus.com/api";

        public async Task<User> AuthenticateUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var userinfo = new User
                {
                    username = username,
                    password = ComputeHash(password)
                };

                var url = $"{baseUrl}/users/GetAuthentication";

                var json = JsonConvert.SerializeObject(userinfo);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content);

                if (response.Result.StatusCode != System.Net.HttpStatusCode.OK) return null;

                var user = JsonConvert.DeserializeObject<User>(await response.Result.Content.ReadAsStringAsync());//.ReadAsAsync<User>().Result;

                return user;
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            using (var client = new HttpClient())
            {
                //var response = await client.GetAsync($"{baseUrl}/products/getproducts");

                //var products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());

                var url = "http://api.dienchanus.com/api/products/getproducts";

                var content = client.GetStringAsync(url);

                var products = JsonConvert.DeserializeObject<List<Product>>(content.Result);

                return products;
            }
        }

        public async Task<List<Order>> GetOrders()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{baseUrl}/orders/getorders");

                var orders = JsonConvert.DeserializeObject<List<Order>>(await response.Content.ReadAsStringAsync());

                return orders;
            }
        }

        public async Task<HttpResponseMessage> DeleteOrder(int orderId)
        {
            var client = new HttpClient();

            var response = await client.DeleteAsync($"{baseUrl}/orders/deleteorder/{orderId}");

            //var result = response.Result.StatusCode;

            return response;
        }


        private string ComputeHash(string password)
        {
            using (var md5Hash = MD5.Create())
            {
                var bytes = Encoding.ASCII.GetBytes(password);

                var data = md5Hash.ComputeHash(bytes);

                var sb = new StringBuilder();

                foreach (var d in data)
                {
                    sb.Append(d.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
