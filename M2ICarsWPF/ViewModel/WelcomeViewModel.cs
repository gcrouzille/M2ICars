﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using M2ICarsWPF;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace M2ICarsWPF.ViewModel
{


    class WelcomeViewModel : ViewModelBase
    {
        private ObservableCollection<User> users;
        private ObservableCollection<Driver> drivers;
        private ObservableCollection<Reservation> reservations;

        private User user;
        private Driver driver;
        private Manager manager = Manager.Instance;

        public WelcomeViewModel()
        {            
            Task.Run(async () => {
                List<User> managerUsers = await manager.InfoUsers();
                Users = new ObservableCollection<User>(managerUsers);
                List<Driver> managerDrivers = await manager.InfoDriver();
                drivers = new ObservableCollection<Driver>(managerDrivers);
            });
          
           
        }

        public User User { get => user; set { user = value; RaisePropertyChanged("User"); } }
        public int UserId { get => User.UserId; set { User.UserId = value; RaisePropertyChanged("UserId"); } }
        public string Firstname { get => User.Firstname; set { User.Firstname = value; RaisePropertyChanged("FirstName"); } }
        public string Lastname { get => User.Lastname; set { User.Lastname = value; RaisePropertyChanged("LastName"); } }
        public DateTime Birthday { get => User.Birthday; set { User.Birthday = value; RaisePropertyChanged("Birthday"); } }
        public string Phone { get => User.Phone; set { User.Phone = value; RaisePropertyChanged("Phone"); } }
        public int Gender { get => User.Gender; set { User.Gender = value; RaisePropertyChanged("Gender"); } }
        public string Email { get => User.Email; set { User.Email = value; RaisePropertyChanged("Email"); } }
        





        public ObservableCollection<User> Users { get => users; set { users = value; RaisePropertyChanged("Users"); } }
        public ObservableCollection<Driver> Drivers { get => drivers; set {drivers = value; RaisePropertyChanged("Drivers");} }
        public ObservableCollection<Reservation> Reservations { get => reservations; set {reservations = value; RaisePropertyChanged("Reservation"); } }
    }

}

        
    

    
    
   



    
