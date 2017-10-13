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

        public Opinion ()
        {

        }
    }
}
