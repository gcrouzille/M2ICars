using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2ICarsASP.Models
{
    public class IndexViewModel
    {
        public string ErrorMsg { get; set; }

        public IndexViewModel(string errorMsg)
        {
            ErrorMsg = errorMsg;
        }
    }
}