using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2ICarsWPF.ViewModel
{
    class UserViewModel:ViewModelBase
    {
        private Manager manager = Manager.Instance;

       
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users { get => users; set { users = value; RaisePropertyChanged("Users"); } }

        private User user;
        public User User { get => user; set { user = value; RaisePropertyChanged("User"); } }
        public int UserId { get => User.UserId; set { User.UserId = value; RaisePropertyChanged("UserId"); } }
        public string Firstname { get => User.Firstname; set { User.Firstname = value; RaisePropertyChanged("FirstName"); } }
        public string Lastname { get => User.Lastname; set { User.Lastname = value; RaisePropertyChanged("LastName"); } }
        public DateTime Birthday { get => User.Birthday; set { User.Birthday = value; RaisePropertyChanged("Birthday"); } }
        public string Phone { get => User.Phone; set { User.Phone = value; RaisePropertyChanged("Phone"); } }
        public int Gender { get => User.Gender; set { User.Gender = value; RaisePropertyChanged("Gender"); } }
        public string Email { get => User.Email; set { User.Email = value; RaisePropertyChanged("Email"); } }
        public string PhotoUrl { get => User.PhotoUrl; set { User.PhotoUrl = value; RaisePropertyChanged("PhotoUrl"); } }
        public string Password { get => User.Password; set { User.Password= value; RaisePropertyChanged("Password"); } }


        public UserViewModel()
        {
            Users = new ObservableCollection<User>(Manager.Instance.Users);
        }

        public void DeleteUser(User user)
        {
            Users.Remove(user);
            manager.DeleteUser(user);
        }
    }
}
