using M2ICarsASP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class SearchDriverController : Controller
    {
        // GET: SearchDriver
        public async Task<ActionResult> Index()
        {
            List<Reservation> ListeReservation = new List<Reservation>();
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
            HttpResponseMessage response = await client.GetAsync($"api/Reservations");

            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                ListeReservation = JsonConvert.DeserializeObject<List<Reservation>>(s);
                

            }

            return View("Index", ListeReservation);
        }

       
    }
    
}