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
        public int ClientId { get => clientId; set => clientId = value; }
        [Required]
        public string LastName { get => lastName; set => lastName = value; }
        [Required]
        public string FirstName { get => firstName; set => firstName = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string Gender { get => gender; set => gender = value; }
        [Required]
        public string Phone { get => phone; set => phone = value; }
        public string PhotoUrl { get => photoUrl; set => photoUrl = value; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get => email; set => email = value; }
        [MinLength(4)]
        public string Password { get => password; set => password = value; }
      

        public Client()
        {
            PhotoUrl = "default.jpg";
        }
    }
}