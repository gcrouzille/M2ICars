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

namespace M2ICarsAPI.Controllers
{
    public class TarifsController : ApiController
    {
        private DB db = new DB();

        // GET: api/Tarifs
        public IQueryable<Tarif> GetTarifs()
        {
            return db.Tarifs;
        }

        // GET: api/Tarifs/5
        [ResponseType(typeof(Tarif))]
        public IHttpActionResult GetTarif(int id)
        {
            Tarif tarif = db.Tarifs.Find(id);
            if (tarif == null)
            {
                return NotFound();
            }

            return Ok(tarif);
        }

        // PUT: api/Tarifs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarif(int id, Tarif tarif)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarif.TarifId)
            {
                return BadRequest();
            }

            db.Entry(tarif).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarifExists(id))
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

        // POST: api/Tarifs
        [ResponseType(typeof(Tarif))]
        public IHttpActionResult PostTarif(Tarif tarif)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tarifs.Add(tarif);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarif.TarifId }, tarif);
        }

        // DELETE: api/Tarifs/5
        [ResponseType(typeof(Tarif))]
        public IHttpActionResult DeleteTarif(int id)
        {
            Tarif tarif = db.Tarifs.Find(id);
            if (tarif == null)
            {
                return NotFound();
            }

            db.Tarifs.Remove(tarif);
            db.SaveChanges();

            return Ok(tarif);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TarifExists(int id)
        {
            return db.Tarifs.Count(e => e.TarifId == id) > 0;
        }
    }
}