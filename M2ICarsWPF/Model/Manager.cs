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


        private List<User> users;
        private List<Driver> drivers;
        private List<Reservation> reservations;

        public List<User> Users { get => users; set => users = value; }
        public List<Driver> Drivers { get => drivers; set => drivers = value; }
        internal List<Reservation> Reservations { get => reservations; set => reservations = value; }


        public async Task<List<User>> InfoUsers()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/User");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<User>>(s);

            }
            return Users;
        }

        public async Task<List<Driver>> InfoDriver()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Drivers");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                Drivers = JsonConvert.DeserializeObject<List<Driver>>(s);

            }
            return Drivers;
        }

        public void Initialize()
        {
            // Initialisation de la liste des réservations/users/drivers
            Task.Run(async () =>
            {
                Reservations = await APIService.Instance.Request<List<Reservation>>("GET", "api/Reservations");
                Users = await APIService.Instance.Request<List<User>>("GET", "api/User");
                Drivers = await APIService.Instance.Request<List<Driver>>("GET", "api/Drivers");
            });
        }

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
           
    }
        
    
}
