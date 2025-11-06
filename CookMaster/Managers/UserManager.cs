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

        //En property som håller reda på den inloggade användaren i programmet
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

        //Returnerar true om inloggade usern är en Admin
        //Annars falsk
        public bool IsAdmin => CurrentUser is AdminUser;
        
        public UserManager()
        {
            
            _users = new List<User>();
            SeedDefaultUsers();
        }

        //Skapar ett par default användare
        private void SeedDefaultUsers()
        {

            _users.Add(new User {UserName = "user", Password = "password", Country = "Sverige", SecurityQuestion = "Vilken var din första skola?", SecurityAnswer = "Fredric" });
            _users.Add(new User { UserName = "hej", Password = "pass", Country = "Sverige", SecurityQuestion = "Vilket är ditt favoritlag?", SecurityAnswer = "Fredric" });
            _users.Add(new AdminUser { UserName = "admin", Password = "password", Country = "Sverige", SecurityQuestion = "Vad heter din mormor?", SecurityAnswer = "Fredric"});

        }

        public bool Login(string username, string password)
        { 
            //Kontrollerar användares username resp password och returnerar true om det stämmer
            //Användaren skickas in som CurrentUser
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

        //Tar emot argument till sina parameterar och skapar en ny användare som läggs till i user-listan
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
            //Returnerar true om användarnamn hittas annars false
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
            //Uppdaterar lösenordet
            
            if(CurrentUser.UserName == username)
            {  
                CurrentUser.Password = password;
            }
        }
        public virtual bool ValidatePassword(string password)
        {
            //Kontrollerar att password innehåller specialtecken, siffra och minst åtta tecken
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
