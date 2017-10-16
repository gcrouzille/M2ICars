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
                opinion.Add(new Opinion(o.OpinionId,o.DriverId, o.UserId, o.Note, o.Comment));
            }

            return opinion.AsQueryable();
        }


        [Route("api/Opinions/Driver/{id}")]
        public IQueryable<Opinion> GetOpinionsDriver(int? id)
        {
            List<Opinion> opinion = new List<Opinion>();

            foreach (Opinion o in db.Opinions)
            {
                if (o.DriverId == id)
                {
                    opinion.Add(new Opinion(o.OpinionId,o.DriverId, o.UserId, o.Note, o.Comment));
                }                
            }
            return opinion.AsQueryable();
        }

        [Route("api/Opinions/Note/{id}")]
        public IQueryable<Opinion> GetOpinionsNote(int? id)
        {
            List<Opinion> opinion = new List<Opinion>();

            foreach (Opinion o in db.Opinions)
            {
                if (o.Note == id)
                {
                    opinion.Add(new Opinion(o.OpinionId, o.DriverId, o.UserId, o.Note, o.Comment));
                }
            }
            return opinion.AsQueryable();
        }

        [Route("api/Opinions/User/{id}")]
        public IQueryable<Opinion> GetOpinionsUser(int? id)
        {
            List<Opinion> opinion = new List<Opinion>();

            foreach (Opinion o in db.Opinions)
            {
                if (o.UserId == id)
                {
                    opinion.Add(new Opinion(o.OpinionId,o.DriverId, o.UserId, o.Note, o.Comment));
                }
            }
            return opinion.AsQueryable();
        }

        
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
        public IHttpActionResult DeleteOpinion(int? id)
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