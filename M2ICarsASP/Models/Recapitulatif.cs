using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2ICarsASP.Models
{
    public class Recapitulatif
    {
     

        public Recapitulatif(Reservation reservation, List<Opinion> listeOpinion)
        {
            Reservation = reservation;
            ListeOpinion = listeOpinion;
           
        }

        public Recapitulatif()
        {

        }

       
        public Reservation Reservation { get; set; }
        public List<Opinion> ListeOpinion{ get; set; }
        public string Time { get; set; }

    }
}