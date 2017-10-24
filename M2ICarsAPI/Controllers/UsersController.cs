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
using M2ICarsAPI.Models;

namespace M2ICarsAPI.Controllers
{
    public class UsersController : ApiController
    {
        private DB db = new DB();

        // GET: api/User
        [Route("api/User")]
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/User/5
        [ResponseType(typeof(User))]
        [Route("api/User/{Id}")]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/User/5
        [ResponseType(typeof(User))]
        [Route("api/User/mail/{email}/{com}")]
        public IHttpActionResult GetUser(string email, string com)
        {
            string recomposedMail = email+"."+com;
            User user = db.Users.First(u => u.Email == recomposedMail);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        [Route("api/User/{Id}")]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }
            
            db.Entry(user).State = EntityState.Modified;
            db.Entry(user).Property(u => u.Password).IsModified = false;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        // PUT: api/User/Pass/5
        [ResponseType(typeof(void))]
        [Route("api/User/Pass/{Id}")]
        public IHttpActionResult PutUser(int id, ChangePasswordModel change)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != change.UserId)
            {
                return BadRequest();
            }

            User user = db.Users.Find(id);
            if (user == null)
                return NotFound();

            if (user.Password == change.OldPassword)
            {
                user.Password = change.NewPassword;
                db.Entry(user).Property(u => u.Password).IsModified = true;
            }
            else
                return BadRequest();

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users       

        [ResponseType(typeof(User))]       
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(User))]
        [Route("api/User/{Id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }


    }
}