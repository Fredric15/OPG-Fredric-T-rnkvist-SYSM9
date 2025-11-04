using CookMaster.Managers;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookMaster.ViewModel
{
    public class UserDetailsViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        private string _newUsername;

        public string NewUsername
        {
            get { return _newUsername; }
            set 
            { 
                _newUsername = value;
                OnPropertyChanged();
            }
        }

        private string _newPassword;
        public string NewPwd
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }
        private string _confirmPwd;
        public string ConfirmPwd
        {
            get { return _confirmPwd; }
            set 
            { 
                _confirmPwd = value;
                OnPropertyChanged();
            }
        }
        private string _newcountry;

        public string NewCountry
        {
            get { return _newcountry; }
            set 
            { 
                _newcountry = value;
                OnPropertyChanged();
            }
        }
        public List<String> Countries { get; set; } = new List<string> { "Sverige", "Finland", "Norge", "Danmark" };
        public List<String> SecurityQ { get; set; } = new List<string> { "Vilken var din första skola?", "Vad heter din mormor?", "Vilket är ditt favoritlag?" };

        private string _errorText;

        public string ErrorText
        {
            get { return _errorText; }
            set 
            { 
                _errorText = value; 
                OnPropertyChanged(); 
            }
        }


        public ICommand SaveCommand { get;}
        public ICommand CancelCommand { get;}

        public event Action SaveUserDetails;
        public event Action CloseWindow;
        public UserDetailsViewModel(UserManager userManager)
        {
            _userManager = userManager;
            
            NewCountry = _userManager.CurrentUser.Country;


            SaveCommand = new RelayCommand(execute => SaveDetails());
            CancelCommand = new RelayCommand(execute => Cancel());
        }



        private void Cancel()
        {
            CloseWindow?.Invoke();
        }

        private void SaveDetails()
        {
            
            if (!string.IsNullOrWhiteSpace(NewUsername))
            {
                if (CheckUsername(NewUsername))
                {
                    System.Windows.MessageBox.Show("Dina ändringar är sparade.");
                    _userManager.CurrentUser.UpdateUsername(NewUsername);
                    CloseWindow?.Invoke();
                }
            }

            if (!string.IsNullOrWhiteSpace(NewPwd))
            {
                if (ValidPwd() && MatchingPwd())
                {
                    System.Windows.MessageBox.Show("Dina ändringar är sparade.");
                    _userManager.CurrentUser.UpdatePassword(NewPwd);
                    CloseWindow?.Invoke();
                }
            }

            if (_userManager.CurrentUser.Country != NewCountry && !string.IsNullOrWhiteSpace(NewCountry)) 
            {

                System.Windows.MessageBox.Show("Dina ändringar är sparade.");
                _userManager.CurrentUser.UpdateCountry(NewCountry);
                CloseWindow?.Invoke();
            
            }

        }
        
        protected bool ValidPwd()
        {
            if (_userManager.ValidatePassword(NewPwd))
            {
                return true;
            }
            else
            {
                ErrorText = "Lösenordet måste vara minst 8 tecken, samt innehålla en siffra och ett specialtecken.";
                return false;
            }

        }

        protected bool MatchingPwd()
        {
            if (NewPwd == ConfirmPwd)
            {
                ErrorText = "";
                return true;
            }
            else
            {
                ErrorText = "Lösenord matchar ej.";
                return false;
            }
        }

        protected bool CheckUsername(string username)
        {
            //Anropar metoden ExisitngUsername och använder ! för att göra det till true om ledigt användarnamn.
            //Kontrollerar även längd.
            
            if (!(_userManager.CheckExistingUsername(username)) && username.Length >= 3)
            {

                return true;
            }
            else
            {
                ErrorText = "Användarnamnet är antingen upptaget eller för kort. Minst tre tecken.";
                return false;
            }

        }
    }
}
