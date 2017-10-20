using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsWPF.ViewModel
{
    class AddReservationViewModel:ViewModelBase
    {
        private Reservation _reservation;
        private Driver _driver;
        private User _user;

        public Reservation Reservation { get { return _reservation; } set { _reservation = value; RaisePropertyChanged("Reservation"); } }
        
        public DateTime Date { get { return Reservation.Date; } set { _reservation.Date = value; RaisePropertyChanged("Date"); } }
        public Reservation.statut Statut { get { return Reservation.Statut; } set { _reservation.Statut = value; RaisePropertyChanged("Statut"); } }
        public string DepartureLocation { get { return Reservation.DepartureLocation; } set { _reservation.DepartureLocation = value; RaisePropertyChanged("DepartureLocation"); } }
        public string ArrivalLocation { get { return Reservation.ArrivalLocation; } set { _reservation.ArrivalLocation = value; RaisePropertyChanged("ArrivalLocation"); } }
        public Driver Driver { get { return _driver; } set { _driver = value; RaisePropertyChanged("Driver"); } }
        public User Client { get { return _user; } set { _user = value; RaisePropertyChanged("Client"); } }
        public int Duration { get { return Reservation.Duration; } set { _reservation.Duration = value; RaisePropertyChanged("Duration"); } }
        public decimal Price { get { return Reservation.Price; } set { _reservation.Price = value; RaisePropertyChanged("Price"); } }

        public AddReservationViewModel()
        {
            Reservation = new Reservation();
        }

        public void Initialize(Reservation r)
        {
            Reservation = r;
            Driver = Manager.Instance.Drivers.First(d => d.DriverId == r.ReservationDriverId);
            Client = Manager.Instance.Users.First(u => u.UserId == r.ClientId);
            RaisePropertyChanged("Reservation");
            RaisePropertyChanged("Date");
            RaisePropertyChanged("Statut");
            RaisePropertyChanged("DepartureLocation");
            RaisePropertyChanged("ArrivalLocation");
            RaisePropertyChanged("Driver");
            RaisePropertyChanged("Client");
            RaisePropertyChanged("Duration");
            RaisePropertyChanged("Price");
        }

        public bool CheckForm()
        {
            if (DepartureLocation == "" || ArrivalLocation == "" || Driver == null || Client == null)
                return false;

            return true;
        }

        public void AddNewReservation()
        {
            Reservation.ReservationDriverId = Driver.DriverId;
            Reservation.ClientId = Client.UserId;

            Manager.Instance.AddReservation(Reservation);
        }

        public void SaveReservation()
        {
            Reservation.ReservationDriverId = Driver.DriverId;
            Reservation.ClientId = Client.UserId;

            Manager.Instance.SaveReservation(Reservation);
        }

        public override string ToString()
        {
            return $"Date : {Date}\nStatut : {Statut}\nDepart : {DepartureLocation}\nArrivée : {ArrivalLocation}\nChauffer : {Driver}\nClient : {Client}\nDurée : {Duration}\nPrix : {Price}\n";
        }
    }
}
