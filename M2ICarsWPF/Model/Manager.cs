using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
                Users = await APIService.Instance.Request<List<User>>("GET", "api/USer");
                Drivers = await APIService.Instance.Request<List<Driver>>("GET", "api/Drivers");
                
            });
         }

#region driver
        public void AddDriver(Driver driver)
        {
            _instance.Drivers.Add(driver);
            Task.Run(async () =>
            {                
                driver = await APIService.Instance.Request<Driver>("POST", "api/Drivers");

            });

        }

        #endregion

#region User
        public void AddUser(User user)
        {
            _instance.Users.Add(user);
            Task.Run(async () =>
            {
                await APIService.Instance.Request<User>("POST", "api/Users");

            });
        }

        public void DeleteUser(User user)
        {
            _instance.Users.Add(user);
            Task.Run(async () =>
            {
                await APIService.Instance.Request<User>("DELETE", "api/Users");

            });
        }

        public void PutUser(User user)
        {
            _instance.Users.Add(user);
            Task.Run(async () =>
            {
                await APIService.Instance.Request<User>("PUT", "api/Users");

            });
        }

#endregion


    }
        
    
}
