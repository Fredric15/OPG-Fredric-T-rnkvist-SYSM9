using CookMaster.Model;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Services
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
        public bool IsAuthenticated => CurrentUser != null;
        public bool Login(string username, string password)
        { /* sätt CurrentUser, returnera true/false */
            return true;
        }
        public void Logout() => CurrentUser = null;
    }
}
