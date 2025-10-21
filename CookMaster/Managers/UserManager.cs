using CookMaster.Model;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Managers
{
    public class UserManager : ViewModelBase
    {
        private User _currentUser;
        private readonly List<User> _users = new();
        public User CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsAuthenticated));
            }
        }

        public UserManager()
        {
            _users = new List<User>();
            SeedDefaultUsers();
        }

        private void SeedDefaultUsers()
        {
            _users.Add(new User {UserName = "admin", Password = "password", Country = "Sverige"});
            _users.Add(new User { UserName = "user", Password = "password", Country = "Sverige" });
        }

        public bool IsAuthenticated => CurrentUser != null;
        public bool Login(string username, string password)
        { /* sätt CurrentUser, returnera true/false */
            return true;
        }
        public void Logout() => CurrentUser = null;
    }
}
