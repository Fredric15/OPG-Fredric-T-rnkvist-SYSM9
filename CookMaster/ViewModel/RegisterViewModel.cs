using CookMaster.Managers;
using CookMaster.Views;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;

namespace CookMaster.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        public event Action<string>? ConfirmNewUser;
        public event Action RequestClose;
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _confirmPw;
        public string ConfirmPw
        {
            get { return _confirmPw; }
            set 
            {   
                _confirmPw = value;
                OnPropertyChanged();
            }
        }

        public List<String> Countries { get; set; } = new List<string> { "Sverige", "Finland", "Norge", "Danmark", "Finland" };
        public List<String> SecurityQ { get; set; } = new List<string> { "Vilken var din första skola?", "Vad heter din mormor?", "Vilket är ditt favoritlag?" };
        
        private string selectedQ;
        public string SelectedQ
        {
            get { return selectedQ; }
            set 
            { 
                selectedQ = value;
                OnPropertyChanged();
            }
        }
        private string _securityAnswer;

        public string SecurityAnswer
        {
            get { return _securityAnswer; }
            set 
            { 
                _securityAnswer = value;
                OnPropertyChanged();
            }
        }


        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                OnPropertyChanged();
            }
        }

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

        public RegisterViewModel(UserManager userManager)
        {
            _userManager = userManager;
            
        }
        
        public RelayCommand NewUserCommand => new RelayCommand(execute => CreateUser(), canExecute => AllInputBoxesChecked());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());
        protected virtual bool AllInputBoxesChecked()
        {
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(ConfirmPw) && !string.IsNullOrWhiteSpace(SelectedCountry) && !string.IsNullOrWhiteSpace(SelectedQ) && !string.IsNullOrWhiteSpace(SecurityAnswer) && CheckUsername(UserName) && MatchingPwd() && ValidPwd())
            {
                
                return true;
                
            }
            else
            {
                return false;
                
            }
            
        }

        protected virtual bool ValidPwd()
        {
            if (_userManager.ValidatePassword(ConfirmPw))
            {
                return true;
            }
            else
            {
                ErrorText = "Lösenordet måste vara minst 8 tecken, samt innehålla en siffra och ett specialtecken.";
                return false;
            }

        }

        protected virtual bool MatchingPwd()
        {
            if (Password == ConfirmPw)
            {
                ErrorText = null;
                return true;
            }
            else
            {
                ErrorText = "Lösenord matchar ej.";
                return false;
            }
        }

        protected virtual bool CheckUsername(string username)
        {
            if (!_userManager.CheckExistingUsername(username))
            {
                
                return true;
            }
            else
            {
                ErrorText = "Användarnamnet är upptaget.";
                return false;
            }
            
        }

        //public event EventHandler CloseRegister;
        //public event EventHandler ConfirmMessage;
        private void CreateUser()
        {
            _userManager.Register(UserName, Password, SelectedCountry, SelectedQ, SecurityAnswer);
            RequestClose?.Invoke();
            ConfirmNewUser?.Invoke("Ny användare har lagts till.");
        }
        private void Cancel()
        {
            RequestClose?.Invoke();
        }
        
    }
}
