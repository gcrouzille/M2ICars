using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsWPF.ViewModel
{
    class ReservationViewModel : ViewModelBase
    {
        private User _client;
        private Driver _driver;

        public ObservableCollection<Reservation> Reservations { get; set; }

        public User Client {
            get { return _client; }
            set { _client = value; RaisePropertyChanged("Client"); }
        }
        public string ClientName { get { return Client.Lastname; } set { Client.Lastname = value; RaisePropertyChanged("ClientName"); } }

        public Driver Driver
        {
            get { return _driver; }
            set { _driver = value; RaisePropertyChanged("Driver"); }
        }


        public ReservationViewModel()
        {
            Reservations = new ObservableCollection<Reservation>(Manager.Instance.Reservations);
            Client = new User();
        }

        public void SetClient(User user)
        {
            Client = user;
        }

        public void SetDriver(Driver driver)
        {
            Driver = driver;
        }

        public void DeleteReservation(Reservation reservation)
        {
            Reservations.Remove(reservation);
            Manager.Instance.DeleteReservation(reservation);
        }
    }
}
