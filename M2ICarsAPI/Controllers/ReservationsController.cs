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
        public IHttpActionResult PostReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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