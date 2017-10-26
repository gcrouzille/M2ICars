using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace M2ICarsASP.Models
{
    public class Opinion
    {
        [Key]
        public int OpinionId { get; set; }       
        public int UserId { get; set; }
        public int Note { get; set; }
        public string Comment { get; set; }               
       
        [IgnoreDataMember]
        public string DriverName { get; set; }
        public int DriverId { get; set; }

        public Opinion()
        {

        }

        public Opinion(int opinionId, int driverId, int userId, int note, string comment)
        {
            OpinionId = opinionId;
            UserId = userId;
            Note = note;
            Comment = comment;
            DriverId = driverId;
        }
    }
}