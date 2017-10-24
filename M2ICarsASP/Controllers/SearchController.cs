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
        public async Task<ActionResult> Index()
        {
            IndexViewModel model = new IndexViewModel("");
            model.Reservations = await APIService.Instance.Request<List<Reservation>>("GET", "api/Reservations");

            foreach(Reservation r in model.Reservations)
            {
                Driver d = await APIService.Instance.Request<Driver>("GET", $"api/Drivers/{r.ReservationDriverId}");
                r.DriverName = d.FirstName +" "+d.LastName;
                
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
                client.DefaultRequestHeaders.Accept.Clear();
                string s = null;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseDepart = await client.GetAsync($"api/geocode/json?latlng={r.DepartureLocation}&key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8");                
                if (responseDepart.IsSuccessStatusCode)
                {
                    s = await responseDepart.Content.ReadAsStringAsync();
                    JObject o = JObject.Parse(s);
                    r.DepartureLocation = (string)o["results"].First["formatted_address"];                    
                }
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseArrival = await client.GetAsync($"api/geocode/json?latlng={r.ArrivalLocation}&key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8");
                if (responseArrival.IsSuccessStatusCode)
                {
                    s = await responseArrival.Content.ReadAsStringAsync();
                    JObject o = JObject.Parse(s);
                    r.ArrivalLocation = (string)o["results"].First["formatted_address"];
                }
            }

            return View(model);
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
            List<Driver> drivers = null;
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                drivers = JsonConvert.DeserializeObject<List<Driver>>(s);                
            }
            
            if (drivers == null || drivers.Count <= 0)
            {
                return View("Index", new IndexViewModel("Aucun chauffeur n'a été trouvé pour le trajet spécifié"));
            }

            SearchResultsViewModel model = new SearchResultsViewModel(drivers, departure, arrival);
            ViewBag.Script = "showDrivers.js";
            ViewBag.GoogleScript = "https://maps.googleapis.com/maps/api/js?key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8&callback=initMap";

            return View(model);
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
            HttpResponseMessage responseDeparture = await clientDeparture.GetAsync($"api/geocode/json?address={departureLocation}&language=en&key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8");
            string departure = null;
            string departureGeocode = null;
            if (responseDeparture.IsSuccessStatusCode)
            {
                departure = await responseDeparture.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(departure);

                string lat = o["results"].First["geometry"]["location"]["lat"].ToString().Replace(',', '.');
                string lng = o["results"].First["geometry"]["location"]["lng"].ToString().Replace(',', '.');
                departureGeocode = lat+ ","+ lng;
            }

            //Conversion du lieu d'arrivée en geocode
            HttpClient clientArrival = new HttpClient();
            clientArrival.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
            clientArrival.DefaultRequestHeaders.Accept.Clear();
            clientArrival.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseArrival = await clientArrival.GetAsync($"api/geocode/json?address={arrivalLocation}&language=en&key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8");
            string arrival = null;
            string arrivalGeocode = null;
            if (responseArrival.IsSuccessStatusCode)
            {
                arrival = await responseArrival.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(arrival);

                string lat = o["results"].First["geometry"]["location"]["lat"].ToString().Replace(',', '.');
                string lng = o["results"].First["geometry"]["location"]["lng"].ToString().Replace(',', '.');
                arrivalGeocode = lat + "," + lng;
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
                resa.DepartureLocation = departureLocation;
                resa.ArrivalLocation = arrivalLocation;
                resa.DriverName = driverRadio;
                return View("Paiement", resa);
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateReservation(string ccName, string ccNumber, string ccCVV, string ccExpirationMonth, string ccExpirationYear)
        {
            //Validation du paiement
            if (ValidatePaiement(ccName, ccNumber, ccCVV, ccExpirationMonth, ccExpirationYear))
                ViewBag.Result = "OK";
            else
                ViewBag.Result = "NOK";

            return View("ReservationValidated");
        }

        public bool ValidatePaiement(string ccName, string ccNumber, string ccCVV, string ccExpirationMonth, string ccExpirationYear)
        {
            return true;
        }
    }
}