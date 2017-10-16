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
        public enum access
        {
            ANALYST,
            ADMIN
        }

        [Key]
        public int AdminId { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public access Access { get; set; }

        public Admin()
        {

        }
    }
}
