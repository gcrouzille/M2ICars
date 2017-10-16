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
        public enum statut
        {
            EN_ATTENTE,
            EN_COURS,
            TERMINE,
        };

        [Key]
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public statut Statut { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }

        [ForeignKey("ReservationDriverId")]
        public virtual Driver ReservationDriver { get; set; }
        public int ReservationDriverId { get; set; }

        [ForeignKey("ClientId")]
        public virtual User Client { get; set; }
        public int ClientId { get; set; }

        public int Duration { get; set; }
        public decimal Price { get; set; }

        public Reservation ()
        {

        }
    }
}
