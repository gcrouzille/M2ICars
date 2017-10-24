using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2ICarsAPI.Models
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}