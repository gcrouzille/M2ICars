using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsDAO
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Password { get; set; }
        public string RegisterState { get; set; }
        public string Availability { get; set; }
        public string CarBrand { get; set; }
        public string CarType { get; set; }
        public string Immatriculation { get; set; }

        public Driver()
        {

        }

    }
}
