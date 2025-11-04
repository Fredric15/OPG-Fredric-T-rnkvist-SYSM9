using CookMaster.Model;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace CookMaster.Managers
{
    public class UserManager : ViewModelBase
    {
        public readonly List<User> _users = new();

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsAdmin));
            }
        }
        private string _twoFactorCode;
        public string TwoFactorCode
        {
            get { return _twoFactorCode; }
            set { _twoFactorCode = value; }
        }

        public UserManager()
        {
            
            _users = new List<User>();
            SeedDefaultUsers();
        }

        private void SeedDefaultUsers()
        {

            _users.Add(new User {UserName = "user", Password = "password", Country = "Sverige", SecurityQuestion = "Vilken var din första skola?", SecurityAnswer = "Fredric" });
            _users.Add(new User { UserName = "hej", Password = "pass", Country = "Sverige", SecurityQuestion = "Vilket är ditt favoritlag?", SecurityAnswer = "Fredric" });
            _users.Add(new AdminUser { UserName = "admin", Password = "password", Country = "Sverige", SecurityQuestion = "Vad heter din mormor?", SecurityAnswer = "Fredric"});

        }

        public bool IsAdmin => CurrentUser is AdminUser;
        public bool Login(string username, string password)
        { /* sätt CurrentUser, returnera true/false */

            foreach (var user in _users)
            {
                if (user.UserName == username && user.Password == password)
                {
                    CurrentUser = user;
                    return true;
                }
            }
            return false;
        }
        public void Logout() => CurrentUser = null;

        public void Register(string username, string password, string country, string securityQ, string securityA)
        {
            
            _users.Add(new User {UserName = username, Password=password,Country = country, SecurityQuestion = securityQ, SecurityAnswer = securityA});
        }

        public bool CheckExistingUsername(string username) 
        {
            //Kontrollerar upptagna användarnamn.
            //Returnerar True om upptaget annars falskt

            foreach (var user in _users)
            {
                if (user.UserName == username)
                    
                    return true;
            }
            return false;
        }
        public bool FindUser(string username)
        {

            foreach (var user in _users)
            {
                if (user.UserName.Equals(username))
                {
                    CurrentUser = user;
                    return true; 
                }
            }
            return false;
        }
        public void ChangePassword(string username, string password)
        {
            
            if(CurrentUser.UserName == username)
            {  
                CurrentUser.Password = password;
            }
        }
        public virtual bool ValidatePassword(string password)
        {
            
            bool ContainsSpecial = false;
            string specialCharacters = "!@#$%^&*()-_=+[{]};:’\"|\\,<.>/?";

            foreach (char c in password)
            {
                if (specialCharacters.Contains(c))
                {
                    ContainsSpecial = true;
                }
            }

            if (password.Length >= 8 && password.Any(char.IsDigit) && ContainsSpecial)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<User> GetAllUsersList()
        {   
            //Filtrerar ut alla användare som inte är AdminUser
            var users = _users.Where(u => u is not AdminUser).ToList();
            return users;
        }
    }
}
