using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M2ICarsASP.Models
{
    public class Client
    {
        private int clientId;
        private string lastName;
        private string firstName;
        private string gender;
        private DateTime birthday;
        private string phone;
        private string photoUrl;
        private string email;
        private string password;


        [Key]
        public int UserId { get => clientId; set => clientId = value; }

        [Required]
        [Display(Name ="Nom")]
        public string LastName { get => lastName; set => lastName = value; }

        [Required]
        [Display(Name = "Prénom")]
        public string FirstName { get => firstName; set => firstName = value; }

        [Display(Name = "Date de Naissance")]
        public DateTime Birthday { get => birthday; set => birthday = value; }

        [Display(Name = "Genre")]
        public string Gender { get => gender; set => gender = value; }

        [Required]
        [Display(Name = "Téléphone")]
        public string Phone { get => phone; set => phone = value; }

        [Display(Name = "Photo")]
        public string PhotoUrl { get => photoUrl; set => photoUrl = value; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse Email")]
        public string Email { get => email; set => email = value; }

        [Required]
        [MinLength(4)]
        [Display(Name = "Mot de Passe")]
        public string Password { get => password; set => password = value; }
      

        public Client()
        {
            PhotoUrl = "default.jpg";
        }
    }
}