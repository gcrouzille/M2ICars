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
            ViewBag.Script = "showDrivers.js";
            ViewBag.GoogleScript = "https://maps.googleapis.com/maps/api/js?key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8&callback=initMap";

            return View(model);
        }

        public ActionResult Paiement()
        {
            Reservation test = new Reservation();
            test.ClientId = 1;
            test.ReservationDriverId = 2;
            test.Date = DateTime.Now;
            test.DepartureLocation = "Lille";
            test.ArrivalLocation = "Roubaix";
            return View("Paiement", test);
        }

        // POST: Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SelectDriver(int driverId, string driverRadio, string departureLocation, string arrivalLocation)
        {
            Reservation newResa = new Reservation();
            newResa.ClientId = (int)Session["clientId"];
            newResa.ReservationDriverId = driverId;
            
            //Conversion du lieu de départ en geocode
            HttpClient clientDeparture = new HttpClient();
            clientDeparture.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
            clientDeparture.DefaultRequestHeaders.Accept.Clear();
            clientDeparture.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseDeparture = await clientDeparture.GetAsync($"api/geocode/json?address={departureLocation}&key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8");
            string departure = null;
            string departureGeocode = null;
            if (responseDeparture.IsSuccessStatusCode)
            {
                departure = await responseDeparture.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(departure);
                departureGeocode = "{\"lat\":" + o["results"].First["geometry"]["location"]["lat"]+ ",\"lng\":"+ o["results"].First["geometry"]["location"]["lng"]+"}";
            }

            //Conversion du lieu d'arrivée en geocode
            HttpClient clientArrival = new HttpClient();
            clientArrival.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
            clientArrival.DefaultRequestHeaders.Accept.Clear();
            clientArrival.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseArrival = await clientArrival.GetAsync($"api/geocode/json?address={arrivalLocation}&key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8");
            string arrival = null;
            string arrivalGeocode = null;
            if (responseArrival.IsSuccessStatusCode)
            {
                arrival = await responseArrival.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(arrival);
                arrivalGeocode = "{\"lat\":" + o["results"].First["geometry"]["location"]["lat"] + ",\"lng\":" + o["results"].First["geometry"]["location"]["lng"] + "}";
            }

            newResa.DepartureLocation = departureGeocode;
            newResa.ArrivalLocation = arrivalGeocode;


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Reservations", newResa);
            
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                Reservation resa;
                resa = JsonConvert.DeserializeObject<Reservation>(s);
                return View("Paiement", resa);
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateReservation()
        {
            return View("ReservationValidated");
        }
    }
}