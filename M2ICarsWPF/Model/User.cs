using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsWPF
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(string firstname, string lastname, DateTime birthday, string phone, int gender, string email, string photoUrl, string password)
        {            
            Firstname = firstname;
            Lastname = lastname;
            Birthday = birthday;
            Phone = phone;
            Gender = gender;
            Email = email;
            PhotoUrl = photoUrl;
            Password = password;
        }
    }
}
