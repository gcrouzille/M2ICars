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
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace M2ICarsAPI.Controllers
{
    public class DriversController : ApiController
    {
        private DB db = new DB();

        // GET: api/Drivers
        public IQueryable<Driver> GetDrivers()
        {
            return db.Drivers;
        }

        // GET: api/Drivers/available
        [Route("api/Drivers/available")]
        public IQueryable<Driver> GetDriversAvailable()
        {
            return db.Drivers.Where(d=>d.Availability == Driver.Available.DISPO);
        }

        // GET: api/Drivers/available
        [Route("api/Drivers/available/{location}")]
        public async Task<IQueryable<Driver>> GetDriversAvailableForLocation(string location)
        {
            List<Driver> driversAvailable = new List<Driver>();

            foreach (Driver driver in db.Drivers)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
                client.DefaultRequestHeaders.Accept.Clear();
                string s = null;
                JObject pos =  JObject.Parse(driver.Location);

                string dest = (string)pos["lat"] + "," + (string)pos["lng"];

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync($"api/distancematrix/json?units=imperial&origins={location}&destinations={dest}&key=AIzaSyCDjKjiTE-YwsyVbG2KY4VVJF5w3F7XWt8");
                if (response.IsSuccessStatusCode)
                {
                    s = await response.Content.ReadAsStringAsync();
                    JObject o = JObject.Parse(s);
                    int duration = (int)o["rows"].First["elements"].First["duration"]["value"];
                    
                    if (duration < 3600)
                    {
                        driversAvailable.Add(driver);
                    }
                }
            }
            
            return driversAvailable.AsQueryable();
        }

        // GET: api/Drivers/waitingValidation
        [Route("api/Drivers/waitingValidation")]
        public IQueryable<Driver> GetDriversWaitingValidation()
        {
            return db.Drivers.Where(d => !d.RegisterState);
        }

        // GET: api/Drivers/5
        [ResponseType(typeof(Driver))]
        public IHttpActionResult GetDriver(int id)
        {
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        // GET: api/Drivers/email
        [ResponseType(typeof(Driver))]
        [Route("api/Driver/mail/{email}/{com}")]
        public IHttpActionResult GetDriver(string email, string com)
        {
            string recomposedMail = email + "." + com;
            Driver driver = db.Drivers.First(u => u.Email == recomposedMail);
            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }



        // PUT: api/Drivers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDriver(int id, Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driver.DriverId)
            {
                return BadRequest();
            }

            db.Entry(driver).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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
        
        // PUT: api/Drivers/5/available
        [ResponseType(typeof(void))]
        [Route("api/Drivers/{id}/available")]
        public IHttpActionResult PutDriverAvailable(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Driver driver = db.Drivers.Find(id);

            if (driver == null)
            {
                return NotFound();
            }

            driver.Availability = Driver.Available.DISPO;
            db.Entry(driver).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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


        // PUT: api/Drivers/5/validateRegistration
        [ResponseType(typeof(void))]
        [Route("api/Drivers/{id}/validateRegistration")]
        public IHttpActionResult PutDriverValidateRegistration(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Driver driver = db.Drivers.Find(id);

            if (driver == null)
            {
                return NotFound();
            }

            driver.RegisterState = true;
            db.Entry(driver).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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

        // POST: api/Drivers
        [ResponseType(typeof(Driver))]
        public IHttpActionResult PostDriver(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drivers.Add(driver);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = driver.DriverId }, driver);
        }

        // DELETE: api/Drivers/5
        [ResponseType(typeof(Driver))]
        public IHttpActionResult DeleteDriver(int id)
        {
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            db.Drivers.Remove(driver);
            db.SaveChanges();

            return Ok(driver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DriverExists(int id)
        {
            return db.Drivers.Count(e => e.DriverId == id) > 0;
        }
    }

    class Position
    {
        public string lat;
        public string lng;

        public Position()
        {

        }
    }
}