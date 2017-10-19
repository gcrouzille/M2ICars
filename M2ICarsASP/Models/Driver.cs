using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M2ICarsASP.Models
{
    public class Driver
    {
        private int driverId;
        private string firstName;
        private string lastName;
        private DateTime birthday;
        private string phone;
        private string gender;
        private string email;
        private string photoUrl;
        private string carBrand;
        private string carType;
        private string carModel;
        private string immatriculation;
        private bool registerState;



        [Key]
        public int DriverId { get => driverId; set => driverId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string PhotoUrl { get => photoUrl; set => photoUrl = value; }
        public string CarBrand { get => carBrand; set => carBrand = value; }
        public string CarType { get => carType; set => carType = value; }
        public string CarModel { get => carModel; set => carModel = value; }
        public string Immatriculation { get => immatriculation; set => immatriculation = value; }
        public bool RegisterState { get => registerState; set => registerState = value; }


      

        public Driver()
        {
            PhotoUrl = "default.jpg";
        }
    
    }
}