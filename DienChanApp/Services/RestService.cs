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
using Xamarin.Forms;
using DienChanApp.Views;

namespace DienChanApp.Services
{
    public class RestService
    {
        private INavigation Navigation;
        public RestService(INavigation navigation)
        {
            Navigation = navigation;
        }

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

                var json = JsonConvert.SerializeObject(userinfo);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync($"{baseUrl}/users/GetAuthentication?apiKey={Settings.ApiKey}", content);

                if (response.Result.StatusCode != System.Net.HttpStatusCode.OK) return null;

                var user = JsonConvert.DeserializeObject<User>(await response.Result.Content.ReadAsStringAsync());//.ReadAsAsync<User>().Result;

                Settings.UserId = user.id;

                Settings.User = MapService.ToViewModel(user);

                Settings.Token = user.token;

                return user;
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //var response = await client.GetAsync($"{baseUrl}/products/getproducts");

                    //var products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());

                    var url = $"{baseUrl}/products/getproducts?token={Settings.Token}&userId={Settings.UserId}";

                    var content = client.GetStringAsync(url);

                    var products = JsonConvert.DeserializeObject<List<Product>>(content.Result);

                    return products;
                }
            }
            catch
            {
                await Navigation.PushAsync(new LoginView());

                return new List<Product>();
            }
        }

        public async Task<HttpResponseMessage> CreateProduct(Product product)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(product);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{baseUrl}/products/createproduct?token={Settings.Token}&userId={Settings.UserId}", content);

                return response;
            }
        }

        public async Task<HttpResponseMessage> UpdateProduct(Product product)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(product);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{baseUrl}/products/updateproduct?token={Settings.Token}&userId={Settings.UserId}", content);

                return response;
            }
        }

        public async Task<HttpResponseMessage> DeleteProduct(int productId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{baseUrl}/products/deleteproduct?token={Settings.Token}&userId={Settings.UserId}&id={productId}");

                return response;
            }
        }

        public async Task<List<Order>> GetOrders()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{baseUrl}/orders/getorders?token={Settings.Token}&userId={Settings.UserId}");

                    var orders = JsonConvert.DeserializeObject<List<Order>>(await response.Content.ReadAsStringAsync());

                    return orders;
                }   
            }
            catch
            {
                await Navigation.PushAsync(new LoginView());

                return new List<Order>();
            }
        }

        public async Task<HttpResponseMessage> DeleteOrder(int orderId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{baseUrl}/orders/deleteorder?token={Settings.Token}&userId={Settings.UserId}&id={orderId}");

                return response;
            }
        }

        public async Task<HttpResponseMessage> UpdateOrder(Order order)
        {
            using(var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(order);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{baseUrl}/orders/updateorder?token={Settings.Token}&userId={Settings.UserId}", content);

                return response;
            }
        }

        public async Task<HttpResponseMessage> CreateOrder(Order order)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(order);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{baseUrl}/orders/createorder?token={Settings.Token}&userId={Settings.UserId}", content);
                
                return response;
            }
        }

        public async Task<HttpResponseMessage> SendOrderReceipt(int orderId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{baseUrl}/orders/sendreport?token={Settings.Token}&userId={Settings.UserId}&orderId={orderId}&email=tunguyen161088@gmail.com");

                return response;
            }
        }

        public async Task<HttpResponseMessage> CreateUser(User user)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(user);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{baseUrl}/users/register?apiKey={Settings.ApiKey}", content);

                return response;
            }
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
