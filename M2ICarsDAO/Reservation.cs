using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
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
        [IgnoreDataMember]
        public virtual Driver ReservationDriver { get; set; }
        public int ReservationDriverId { get; set; }

        [ForeignKey("ClientId")]
        [IgnoreDataMember]
        public virtual User Client { get; set; }
        public int ClientId { get; set; }

        public int Duration { get; set; }
        public decimal Price { get; set; }

        public Reservation ()
        {

        }

        public Reservation(int id, DateTime date, statut statut, string departure, string arrival, int driverId, int clientId, int duration, decimal price)
        {
            ReservationId = id;
            Date = date;
            Statut = statut;
            DepartureLocation = departure;
            ArrivalLocation = arrival;
            ReservationDriverId = driverId;
            ClientId = clientId;
            Duration = duration;
            Price = price;
        }
    }
}
