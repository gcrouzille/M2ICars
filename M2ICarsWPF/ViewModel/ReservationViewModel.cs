using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsWPF.ViewModel
{
    class ReservationViewModel
    {
        public ObservableCollection<Reservation> Reservations { get; set; }

        public ReservationViewModel()
        {
            Reservations = new ObservableCollection<Reservation>(Manager.Instance.Reservations);
        }
    }
}
