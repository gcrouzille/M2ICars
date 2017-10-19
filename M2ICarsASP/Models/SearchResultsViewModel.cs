using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2ICarsASP.Models
{
    public class SearchResultsViewModel
    {
        public List<Driver> Drivers { get; set; }
        public string departure { get; set; }
        public string arrival { get; set; }

        public SearchResultsViewModel(List<Driver> drivers, string start, string end)
        {
            Drivers = drivers;
            departure = start;
            arrival = end;
        }
    }
}