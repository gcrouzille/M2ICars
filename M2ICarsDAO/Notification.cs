using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsDAO
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public bool IsDriverNotification { get; set; }
        public int Userid { get; set; }
        public string Message { get; set; }
        
        public Notification()
        {

        }
    }
}
