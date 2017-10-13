using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsDAO
{
    public class User
    {
        [Key]
        public int ClientId { get; set; }              
        public string Firstname { get; set; }        
        public string Lastname { get; set; }                  
        public DateTime Birthday { get; set; }        
        public string Phone { get; set; }
        public int Gender { get; set; }              
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Password { get; set; }
       
        public virtual Driver Driver { get;set; }
        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        public User()
        {

        }
    }
}
