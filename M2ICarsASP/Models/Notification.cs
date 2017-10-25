using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2ICarsASP.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public bool IsDriverNotification { get; set; }
        public int Userid { get; set; }
        public string Message { get; set; }
    }
}