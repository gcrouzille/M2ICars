using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using M2ICarsDAO;
using M2ICarsAPI.Controllers.JWT;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace M2ICarsAPI.Controllers
{
    public class ReservationsController : ApiController
    {
        private DB db = new DB();

        // GET: api/Reservations
        public IQueryable<Reservation> GetReservations()
        {
            List<Reservation> resas = new List<Reservation>();

            foreach (Reservation r in db.Reservations.ToList())
            {
                resas.Add(new Reservation(r.ReservationId, r.Date, r.Statut, r.DepartureLocation, r.ArrivalLocation, r.ReservationDriverId, r.ClientId, r.Duration, r.Price));
            }
            return resas.AsQueryable();
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        

        // GET: api/Reservations/EnCours
        [Route("api/Reservations/EnCours")]
        public IQueryable<Reservation> GetReservationsEnCours()
        {
            return db.Reservations.Where(r => r.Statut == Reservation.statut.EN_COURS);
        }


        // GET: api/Reservations/Avances
        [Route("api/Reservations/Avances")]
        public IQueryable<Reservation> GetReservationAvances()
        {
            return db.Reservations.Where(r => r.Statut == Reservation.statut.EN_ATTENTE);
        }


        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.ReservationId)
            {
                return BadRequest();
            }

            db.Entry(reservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        [TokenAuthenticate(MemberShipProvider.Role.USER)]
        public async Task<IHttpActionResult> PostReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;

            string start = reservation.DepartureLocation;
            string end = reservation.ArrivalLocation;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/distancematrix/json?units=imperial&origins={start}&destinations={end}&key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(s);
                int duration = (int)o["rows"].First["elements"].First["duration"]["value"];
                int distance = (int)o["rows"].First["elements"].First["distance"]["value"];
                
                reservation.Duration = duration;
                
                Driver driver = db.Drivers.First(d => d.DriverId == reservation.ReservationDriverId);
                reservation.Price = (distance/1000) * db.Tarifs.First(t => t.CarType == driver.CarType).Price;
            }

            db.Reservations.Add(reservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reservation.ReservationId }, reservation);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            db.SaveChanges();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.ReservationId == id) > 0;
        }
    }
}