using M2ICarsDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace M2ICarsAPI.Controllers
{
    public class StatsController : ApiController
    {
        private DB db = new DB();

        // GET: api/Stats/CA
        [ResponseType(typeof(Reservation))]
        [Route("api/Stats/CA")]
        public IHttpActionResult GetCA()
        {
            decimal CA = 24000M;

            return Ok(CA);
        }

        // GET: api/Stats/CA/1
        [ResponseType(typeof(Reservation))]
        [Route("api/Stats/CA/{year}/{month}")]
        public IHttpActionResult GetCAByMonth(int year, int month)
        {
            decimal CA = 1000M;

            return Ok(CA);
        }

        // GET: api/Stats/CA/2017
        [ResponseType(typeof(Reservation))]
        [Route("api/Stats/CA/{year}")]
        public IHttpActionResult GetCAByYear(int year)
        {
            decimal CA = 12000M;

            return Ok(CA);
        }


    }
}
