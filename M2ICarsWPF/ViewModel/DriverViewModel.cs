using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsWPF.ViewModel
{
    class DriverViewModel :ViewModelBase
    {
        private Manager manager = Manager.Instance;

        #region user
        private ObservableCollection<Driver> drivers;
        public ObservableCollection<Driver> Drivers { get => drivers; set { drivers = value; RaisePropertyChanged("Drivers"); } }

        private Driver driver;
        public Driver Driver { get => driver; set { driver = value; RaisePropertyChanged("Driver"); } }

        public int UserId { get => driver.DriverId; set { driver.DriverId = value; RaisePropertyChanged("DriverId"); } }
        public string Firstname { get => driver.Firstname; set { driver.Firstname = value; RaisePropertyChanged("FirstName"); } }
        public string Lastname { get => driver.Lastname; set { driver.Lastname = value; RaisePropertyChanged("LastName"); } }
        public Driver.Available Availability { get; set; }
        public bool RegisterState { get => Driver.RegisterState; set { Driver.RegisterState = value; RaisePropertyChanged("RegisterState"); } }
        public DateTime Birthday { get => driver.Birthday; set { driver.Birthday = value; RaisePropertyChanged("Birthday"); } }
        public string Phone { get => driver.Phone; set { driver.Phone = value; RaisePropertyChanged("Phone"); } }
        public int Gender { get => driver.Gender; set { driver.Gender = value; RaisePropertyChanged("Gender"); } }
        public string Email { get => driver.Email; set { driver.Email = value; RaisePropertyChanged("Email"); } }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public Driver.TypeOfCar CarType { get; set; }
        public string Immatriculation { get; set; }

        #endregion

        public DriverViewModel()
        {
            Drivers = new ObservableCollection<Driver>(Manager.Instance.Drivers);
            
        }

        public void DeleteDriver(Driver driver)
        {
            Drivers.Remove(driver);
            manager.DeleteDriver(driver);
        }

    }
}

