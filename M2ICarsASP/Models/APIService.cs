using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsASP.Models
{
    public class APIService
    {
        private static APIService _instance;
        private static readonly object _lock = new object();

        public static APIService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new APIService();
                    return _instance;
                }
            }
        }

        private string Token { get; set; }

        private APIService()
        {

        }

        public async Task<bool> GetToken(string username, string password)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"api/Account/Admin/Authenticate?mail={username}&password={password}");

            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<string>();
                if (s != "TokenError")
                {
                    Token = s;
                    return true;
                }
            }
            return false;
        }

        public async Task<T> Request<T>(string type, string ressource, params T[] value)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            HttpResponseMessage response = null;
            switch (type)
            {
                case "GET":
                    response = await client.GetAsync(ressource);
                    break;
                case "POST":
                    response = await client.PostAsJsonAsync<T>(ressource, value[0]);
                    break;
                case "PUT":
                    response = await client.PutAsJsonAsync<T>(ressource, value[0]);
                    break;
                case "DELETE":
                    response = await client.DeleteAsync(ressource);
                    break;
                default:
                    break;
            }
            T retour = default(T);
            if (response != null && response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                retour = JsonConvert.DeserializeObject<T>(s);
            }
            return retour;
        }
    }
}