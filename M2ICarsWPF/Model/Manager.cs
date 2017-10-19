using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsWPF
{
    public class Manager
    {
        private static Manager _instance;

        private static readonly object _lock = new object();

        public static Manager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Manager();
                    return _instance;
                }
            }
        }



        private Manager()
        {

        }


        private List<User> users;
        private List<Driver> drivers;
        private List<Reservation> reservations;

        public List<User> Users { get => users; set => users = value; }
        public List<Driver> Drivers { get => drivers; set => drivers = value; }
        internal List<Reservation> Reservations { get => reservations; set => reservations = value; }


        public async Task<List<User>> InfoUsers()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/User");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<User>>(s);

            }
            return Users;
        }

        public async Task<List<Driver>> InfoDriver()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Drivers");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                Drivers = JsonConvert.DeserializeObject<List<Driver>>(s);

            }
            return Drivers;
        }

     
           
    }
        
    
}
