using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsDAO
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        public string Mail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string Password { get; set; }

        public int Access { get; set; }

        public Admin()
        {

        }
    }
}
