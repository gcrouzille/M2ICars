using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsDAO
{
    public class Opinion
    {
        [Key]
        public int OpinionId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public int Note { get; set; }
        public string Comment { get; set; }

        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }
        public int DriverId { get; set; }

        public Opinion ()
        {

        }

        public Opinion(int driverId, int userId, int note, string comment )
        {
            UserId = userId;
            Note = note;
            Comment = comment;
            DriverId = driverId;
        }
    }
}
