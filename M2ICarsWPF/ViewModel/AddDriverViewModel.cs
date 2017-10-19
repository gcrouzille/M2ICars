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
        private Driver driver;

        public RelayCommand<Window> CommandAdd { get; }

        public Window Wp;

        public Driver Driver { get => driver; set { driver= value; RaisePropertyChanged("Driver"); } }
        public string FirstName { get => Driver.Firstname ; set { Driver.Firstname = value; RaisePropertyChanged("FirstName"); } }
        public string LastName { get => Driver.Lastname; set { Driver.Lastname = value; RaisePropertyChanged("LastName"); } }
        public DateTime Birthday { get => Driver.Birthday; set { Driver.Birthday = value; RaisePropertyChanged("Birthday"); } }
        public string Phone { get => Driver.Phone; set { Driver.Phone = value; RaisePropertyChanged("Phone"); } }
        public int Gender{ get => Driver.Gender; set { Driver.Gender = value; RaisePropertyChanged("Gender"); } }
        public string Email { get => Driver.Email; set { Driver.Email = value; RaisePropertyChanged("Email"); } }
        public string PhotoUrl { get => Driver.PhotoUrl; set { Driver.PhotoUrl = value; RaisePropertyChanged("PhotoUrl"); } }
        public string PAssword { get => Driver.Password; set { Driver.Password= value; RaisePropertyChanged("Password"); } }
        public string CarBrand { get => Driver.CarBrand;  set { Driver.CarBrand = value; RaisePropertyChanged("CarBrand"); } }
        public Driver.TypeOfCar CarType  { get => Driver.CarType; set { Driver.CarType = value; RaisePropertyChanged("CarType"); } }
        public string Immatriculation { get => Driver.Immatriculation; set { Driver.Immatriculation = value; RaisePropertyChanged("Immatriculation"); } }
        

        public AddDriverViewModel()
        {            
            CommandAdd = new RelayCommand<Window>(Add);
        }

        public void Add(Window w)
        {
            driver.AddToBase();
            MessageBox.Show("Le Produit a été ajouté avec succés");
            w.Close();
        }

    }
}
