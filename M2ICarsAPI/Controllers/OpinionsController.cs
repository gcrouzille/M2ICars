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
    public class OpinionsController : ApiController
    {
        private DB db = new DB();

        // GET: api/Opinions
        public IQueryable<Opinion> GetOpinions()
        {
            List<Opinion> opinion = new List<Opinion>();

            foreach (Opinion o in db.Opinions.ToList())
            {
                opinion.Add(new Opinion(o.DriverId, o.UserId, o.Note, o.Comment));
            }

            return opinion.AsQueryable();
        }
    }

        //[ResponseType(typeof(Opinion))]
        //public IHttpActionResult GetOpinion(int id)
        //{
                 
        //    return Ok(db.Opinions.Where(o => o.DriverId == id));
        //}

        //// GET: api/Opinions/5
        //[ResponseType(typeof(Opinion))]
        //public IHttpActionResult GetOpinion(int id)
        //{
        //    Opinion opinion = db.Opinions.Find(id);
        //    if (opinion == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(opinion);
        //}


        // PUT: api/Opinions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOpinion(int id, Opinion opinion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != opinion.OpinionId)
            {
                return BadRequest();
            }

            db.Entry(opinion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpinionExists(id))
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

        // POST: api/Opinions
        [ResponseType(typeof(Opinion))]
        public IHttpActionResult PostOpinion(Opinion opinion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Opinions.Add(opinion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = opinion.OpinionId }, opinion);
        }

        // DELETE: api/Opinions/5
        [ResponseType(typeof(Opinion))]
        public IHttpActionResult DeleteOpinion(int id)
        {
            Opinion opinion = db.Opinions.Find(id);
            if (opinion == null)
            {
                return NotFound();
            }

            db.Opinions.Remove(opinion);
            db.SaveChanges();

            return Ok(opinion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OpinionExists(int id)
        {
            return db.Opinions.Count(e => e.OpinionId == id) > 0;
        }
    }
}