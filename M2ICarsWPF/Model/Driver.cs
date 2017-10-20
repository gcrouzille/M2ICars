using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsWPF
{   
       public class Driver
        {
            public enum Available
            {
                NON_DISPO,
                OCCUPE,
                DISPO
            }

            public enum TypeOfCar
            {
                ECONOMIQUE,
                PREMIUM
            }

        
            public int DriverId { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public DateTime Birthday { get; set; }
            public string Phone { get; set; }
            public int Gender { get; set; }
            public string Email { get; set; }
            public string PhotoUrl { get; set; }
            public string Password { get; set; }
            public bool RegisterState { get; set; }
            public Available Availability { get; set; }
            public string CarBrand { get; set; }
            public string CarModel { get; set; }
             public TypeOfCar CarType { get; set; }
            public string Immatriculation { get; set; }

            public Driver()
            {

            }

        public Driver(int driverId, string firstname, string lastname, DateTime birthday, string phone, int gender, string email, string photoUrl, string password, bool registerState, Available availability, string carBrand, TypeOfCar carType, string immatriculation)
        {
            DriverId = driverId;
            Firstname = firstname;
            Lastname = lastname;
            Birthday = birthday;
            Phone = phone;
            Gender = gender;
            Email = email;
            PhotoUrl = photoUrl;
            Password = password;
            RegisterState = registerState;
            Availability = availability;
            CarBrand = carBrand;
            CarType = carType;
            Immatriculation = immatriculation;
        }

       
    }
    
}
