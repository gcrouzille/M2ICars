using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsDAO
{
    public class Tarif
    {
        [Key]
        public int TarifId { get; set; }
        public Driver.TypeOfCar CarType { get; set; }
        public decimal Price { get; set; }        

        public Tarif()
        {
           
        }
    }
}
