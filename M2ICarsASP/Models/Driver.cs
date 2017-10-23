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
        private string lastName;
        private string firstName;
        private DateTime birthday;
        private string location;
        private string phone;
        private string gender;
        private string photoUrl;
        private string carBrand;
        private string carType;
        private string carModel;
        private string immatriculation;
        private bool registerState;
        private string email;
        private string password;



        [Key]
        public int DriverId { get => driverId; set => driverId = value; }

        [Required]
        [Display(Name = "Nom")]
        public string LastName { get => lastName; set => lastName = value; }

        [Required]
        [Display(Name = "Prénom")]
        public string FirstName { get => firstName; set => firstName = value; }

        [Display(Name = "Date De Naissance")]
        public DateTime Birthday { get => birthday; set => birthday = value; }

        [Required]
        [Display(Name = "Téléphone")]
        public string Phone { get => phone; set => phone = value; }

        [Display(Name = "Genre")]
        public string Gender { get => gender; set => gender = value; }

        [Display(Name = "Photo")]
        public string PhotoUrl { get => photoUrl; set => photoUrl = value; }

        [Required]
        [Display(Name = "Marque du véhicule")]
        public string CarBrand { get => carBrand; set => carBrand = value; }

        [Required]
        [Display(Name = "Type de véhicule")]
        public string CarType { get => carType; set => carType = value; }

        [Required]
        [Display(Name = "Modèle")]
        public string CarModel { get => carModel; set => carModel = value; }

        [Required]
        [Display(Name = "N° Immatriculation")]
        public string Immatriculation { get => immatriculation; set => immatriculation = value; }

        [Display(Name = "Licence Validée")]
        public bool RegisterState { get => registerState; set => registerState = value; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse Email")]
        public string Email { get => email; set => email = value; }

        [Required]
        [MinLength(4)]
        [Display(Name = "Mot de Passe")]
        public string Password { get => password; set => password = value; }

        public string Location { get => location; set => location = value; }

        public Driver()
        {
            PhotoUrl = "default.jpg";
        }
    
    }
}