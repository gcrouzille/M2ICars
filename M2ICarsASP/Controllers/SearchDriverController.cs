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
        public async Task<ActionResult> Index(string departure)
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

            foreach (Reservation r in ListeReservation)
            {
                Client c = await APIService.Instance.Request<Client>("GET", $"api/User/{r.ClientId}");
                r.ClientName = c.FirstName + " " + c.LastName;



                //Conversion du lieu de départ en geocode
                HttpClient clientDeparture = new HttpClient();
                clientDeparture.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
                clientDeparture.DefaultRequestHeaders.Accept.Clear();
                clientDeparture.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseDeparture = await clientDeparture.GetAsync($"api/geocode/json?address={r.DepartureLocation}&language=en&key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8");
                
                string departureGeocode = null;
                if (responseDeparture.IsSuccessStatusCode)
                {
                    departure = await responseDeparture.Content.ReadAsStringAsync();
                    JObject o = JObject.Parse(departure);

                    string lat = o["results"].First["geometry"]["location"]["lat"].ToString().Replace(',', '.');
                    string lng = o["results"].First["geometry"]["location"]["lng"].ToString().Replace(',', '.');

                    //departureGeocode = lat + "," + lng;
                    departureGeocode = $"{{\"lat\":{lat},\"lng\":{lng}}}";
                }

                r.DepartureLocation = departureGeocode;
            }



            //ListeReservation = ListeReservation.FindAll(r => r.Duration > 30);

            return View("Index", ListeReservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SelectClient(string clientRadio)
        {           

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
            HttpResponseMessage response = await client.GetAsync($"api/Reservations/{clientRadio}");
            Reservation reservation;
            if (response.IsSuccessStatusCode)
            {
                
                s = await response.Content.ReadAsStringAsync();
                reservation = JsonConvert.DeserializeObject<Reservation>(s);
                reservation.ClientName = clientRadio;

                Client c = await APIService.Instance.Request<Client>("GET", $"api/User/{reservation.ClientId}");
                reservation.ClientName = c.FirstName + " " + c.LastName;
                Driver d = await APIService.Instance.Request<Driver>("GET", $"api/Drivers/{reservation.ReservationDriverId}");
                reservation.DriverName = d.FirstName + " " + d.LastName;

                List<Opinion> opinions;
                opinions = await APIService.Instance.Request<List<Opinion>>("GET", $"api/Opinions/User/{reservation.ClientId}");
                           
                Recapitulatif recap = new Recapitulatif(reservation,opinions );


                return View("Recap",recap);
            }
                        
            return View("index");
        }
           
    }


}


       
    
    
