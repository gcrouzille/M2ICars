using M2ICarsWPF.View;
using M2ICarsWPF.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsWPF
{
    public class Manager
    {
        private static Manager _instance;

        private static readonly object _lock = new object();

        public static Manager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Manager();
                    return _instance;
                }
            }
        }
        private Manager()
        {

        }

        public List<Reservation> Reservations { get; set; }
        public List<User> Users { get; set; }
        public List<Driver> Drivers { get; set; }


   

        public void Initialize()
        {
            //Initialisation de la liste des réservations
            Task.Run(async () =>
            {
                Reservations = await APIService.Instance.Request<List<Reservation>>("GET", "api/Reservations");
                Users = await APIService.Instance.Request<List<User>>("GET", "api/User");
                Drivers = await APIService.Instance.Request<List<Driver>>("GET", "api/Drivers");
                
            });
         }

#region driver
        public void AddDriver(Driver driver)
        {
            Drivers.Add(driver);
            (((App.Current.MainWindow as MainWindow).MainFrame.Content as DriverManagement).DataContext as DriverViewModel).Drivers.Add(driver);
            Task.Run(async () =>
            {                
                driver = await APIService.Instance.Request("POST", "api/Drivers",driver);

            });

        }

        public void SaveDriver(Driver driver)
        {
            int i = Drivers.IndexOf(driver);
            Drivers.Remove(driver);
            Drivers.Insert(i, driver);

            DriverViewModel vm = (((App.Current.MainWindow as MainWindow).MainFrame.Content as DriverManagement).DataContext as DriverViewModel);
            i = vm.Drivers.IndexOf(driver);
            vm.Drivers.Remove(driver);
            vm.Drivers.Insert(i, driver);
            vm.RaisePropertyChanged("Drivers");

            Task.Run(async () =>
            {
                await APIService.Instance.Request("PUT", $"api/Drivers/{driver.DriverId}", driver);
            });
        }

        public void DeleteDriver(Driver driver)
        {
            Drivers.Remove(driver);
            Task.Run(async () =>
            {
                await APIService.Instance.Request<Driver>("DELETE", $"api/Drivers/{driver.DriverId}");
            });
        }

        #endregion

        #region User
        public void AddUser(User user)
        {
            Users.Add(user);
            (((App.Current.MainWindow as MainWindow).MainFrame.Content as UserManagement).DataContext as UserViewModel).Users.Add(user);
            Task.Run(async () =>
            {
                await APIService.Instance.Request("POST", "api/Users",user);

            });
        }

        public void DeleteUser(User user)
        {
            Users.Remove(user);
            Task.Run(async () =>
            {
                await APIService.Instance.Request<User>("DELETE", $"api/User/{user.UserId}");

            });
        }


        public void SaveUser(User user)
        {
            int i = Users.IndexOf(user);
            Users.Remove(user);
            Users.Insert(i,user);

            UserViewModel vm = (((App.Current.MainWindow as MainWindow).MainFrame.Content as UserManagement).DataContext as UserViewModel);
            i = vm.Users.IndexOf(user);
            vm.Users.Remove(user);
            vm.Users.Insert(i, user);
            vm.RaisePropertyChanged("Users");

            Task.Run(async () =>
            {
                await APIService.Instance.Request("PUT", $"api/User/{user.UserId}", user);
            });
        }
#endregion

        #region Reservation

        public void AddReservation(Reservation r)
        {
            Reservations.Add(r);
            (((App.Current.MainWindow as MainWindow).MainFrame.Content as ReservationManagement).DataContext as ReservationViewModel).Reservations.Add(r);
            Task.Run(async () =>
            {
                await APIService.Instance.Request("POST", $"api/Reservations", r);
            });
        }

        public void SaveReservation(Reservation r)
        {
            int i = Reservations.IndexOf(r);
            Reservations.Remove(r);
            Reservations.Insert(i, r);

            ReservationViewModel vm = (((App.Current.MainWindow as MainWindow).MainFrame.Content as ReservationManagement).DataContext as ReservationViewModel);
            i = vm.Reservations.IndexOf(r);
            vm.Reservations.Remove(r);
            vm.Reservations.Insert(i,r);
            vm.RaisePropertyChanged("Reservations");

            Task.Run(async () =>
            {
                await APIService.Instance.Request("PUT", $"api/Reservations/{r.ReservationId}", r);
            });
        }

        public void DeleteReservation(Reservation r)
        {
            Reservations.Remove(r);
            Task.Run(async () =>
            {
                await APIService.Instance.Request<Reservation>("DELETE", $"api/Reservations/{r.ReservationId}");
            });
        }
           
#endregion


    }
        
    
}
