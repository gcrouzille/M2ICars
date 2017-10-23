using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace M2ICarsWPF.ViewModel
{
    class AddDriverViewModel : ViewModelBase
    {
        private Manager manager = Manager.Instance;

        private Driver driver;

        public Driver Driver { get => driver; set { driver = value; RaisePropertyChanged("Driver"); } }
        public string FirstName { get => Driver.Firstname; set { Driver.Firstname = value; RaisePropertyChanged("FirstName"); } }
        public string LastName { get => Driver.Lastname; set { Driver.Lastname = value; RaisePropertyChanged("LastName"); } }
        public DateTime Birthday { get => Driver.Birthday; set { Driver.Birthday = value; RaisePropertyChanged("Birthday"); } }
        public string Phone { get => Driver.Phone; set { Driver.Phone = value; RaisePropertyChanged("Phone"); } }
        public int Gender { get => Driver.Gender; set { Driver.Gender = value; RaisePropertyChanged("Gender"); } }
        public string Email { get => Driver.Email; set { Driver.Email = value; RaisePropertyChanged("Email"); } }
        public string PhotoUrl { get => Driver.PhotoUrl; set { Driver.PhotoUrl = value; RaisePropertyChanged("PhotoUrl"); } }
        public string PAssword { get => Driver.Password; set { Driver.Password = value; RaisePropertyChanged("Password"); } }
        public string CarBrand { get => Driver.CarBrand; set { Driver.CarBrand = value; RaisePropertyChanged("CarBrand"); } }
        public string CarModel { get => Driver.CarModel; set { Driver.CarModel = value; RaisePropertyChanged("CarModel"); } }
        public Driver.TypeOfCar CarType { get => Driver.CarType; set { Driver.CarType = value; RaisePropertyChanged("CarType"); } }
        public string Immatriculation { get => Driver.Immatriculation; set { Driver.Immatriculation = value; RaisePropertyChanged("Immatriculation"); } }

        public AddDriverViewModel()
        {
            driver = new Driver();
        }

        public void Initialize(Driver driver)
        {
            Driver = driver;
            RaisePropertyChanged("Driver");
            RaisePropertyChanged("FirstName");
            RaisePropertyChanged("LastName");
            RaisePropertyChanged("Birthday");
            RaisePropertyChanged("Phone");
            RaisePropertyChanged("Email");
            RaisePropertyChanged("PhotoUrl");
            RaisePropertyChanged("Password");
            RaisePropertyChanged("CarBrand");
            RaisePropertyChanged("CarModel");
            RaisePropertyChanged("CarType");
            RaisePropertyChanged("Immatriculation");
        }

        public void SaveDriver()
        {
            manager.SaveDriver(Driver);

        }

        public void AddNewDriver()
        {
            manager.AddDriver(Driver);
        }

        public void DeleteDriver(Window w)
        {
            manager.DeleteDriver(Driver);
            w.Close();
        }

    }
}
