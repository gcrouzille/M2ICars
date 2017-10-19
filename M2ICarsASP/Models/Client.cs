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
        private string firstName;
        private string lastName;
        private DateTime birthday;
        private string phone;
        private string gender;
        private string email;
        private string photoUrl;

        [Key]
        public int ClientId { get => clientId; set => clientId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string PhotoUrl { get => photoUrl; set => photoUrl = value; }
      

        public Client()
        {
            PhotoUrl = "default.jpg";
        }
    }
}