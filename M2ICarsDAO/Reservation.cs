using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsDAO
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public virtual Driver Driver { get; set; }
        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public virtual User Client { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public int Duration;
        public decimal Price;

        public Reservation ()
        {

        }
    }
}
