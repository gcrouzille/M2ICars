using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2ICarsASP.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public int ReservationDriverId { get; set; }
        public int ClientId { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }

        public Reservation()
        {
            Date = DateTime.Now;
        }
    }
}