using M2ICarsASP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace M2ICarsASP.Controllers
{
    public class SearchController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        
        // POST: Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(string departure, string arrival)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Drivers/available/{departure}");
            List<Driver> drivers;
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                drivers = JsonConvert.DeserializeObject<List<Driver>>(s);                
            }
            else
                drivers = new List<Driver>();

            SearchResultsViewModel model = new SearchResultsViewModel(drivers, departure, arrival);

            return View(model);
        }

        // POST: Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SelectDriver(string driverRadio)
        {
            
            return $"vous avez choisi {driverRadio}";
        }
    }
}