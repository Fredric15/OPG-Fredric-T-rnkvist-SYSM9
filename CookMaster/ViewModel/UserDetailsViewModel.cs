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

        private string _NewSecurityQ;
        public string NewSecurityQ
        {
            get { return _NewSecurityQ; }
            set 
            { 
                _NewSecurityQ = value;
                OnPropertyChanged();
            }
        }

        private string _newSecurityA;

        public string NewSecurityA
        {
            get { return _newSecurityA; }
            set 
            { 
                _newSecurityA = value;
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
            NewSecurityQ = _userManager.CurrentUser.SecurityQuestion;
            NewSecurityA = _userManager.CurrentUser.SecurityAnswer;

            SaveCommand = new RelayCommand(execute => SaveDetails());
            CancelCommand = new RelayCommand(execute => Cancel());
        }



        private void Cancel()
        {
            CloseWindow?.Invoke();
        }

        private void SaveDetails()
        {
            //Här uppdateras den användarinfo som användaren har valt att fylla i på nytt i fönstret
            
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

            if (_userManager.CurrentUser.SecurityQuestion != NewSecurityQ && !string.IsNullOrWhiteSpace(NewSecurityQ))
            {

                System.Windows.MessageBox.Show("Dina ändringar är sparade.");
                _userManager.CurrentUser.UpdateSecurityQ(NewSecurityQ);
                CloseWindow?.Invoke();
            }

            if (_userManager.CurrentUser.SecurityAnswer != NewSecurityA && !string.IsNullOrWhiteSpace(NewSecurityA))
            {

                System.Windows.MessageBox.Show("Dina ändringar är sparade.");
                _userManager.CurrentUser.UpdateSecurityA(NewSecurityA);
                CloseWindow?.Invoke();
            }
        }

        private bool ValidPwd()
        {
            //Kontrollerar att det är rätt format i båda passwordboxarna
            
            if (_userManager.ValidatePassword(NewPwd) || _userManager.ValidatePassword(ConfirmPwd))
            {
                ErrorText = "";
                return true;
            }
            else
            {
                ErrorText = "Lösenordet måste vara minst 8 tecken, samt innehålla en siffra och ett specialtecken.";
                return false;
            }

        }

        private bool MatchingPwd()
        {
            //Kontrollerar att båda lösenorden matchar
            
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
        private bool CheckUsername(string username)
        {
            //Kontrollerar ifall användarnamnet är upptaget eller ej
            //Kontrollerar även att minst antal tecken stämmer
            if (!_userManager.CheckExistingUsername(username))
            {
                if (username.Length >= 3)
                {
                    return true;
                }
                else
                {
                    ErrorText = "Användarnamnet måste vara minst tre tecken.";
                    return false;
                }
            }
            else
            {
                ErrorText = "Användarnamnet är upptaget.";
                return false;
            }
        }
    }
}
