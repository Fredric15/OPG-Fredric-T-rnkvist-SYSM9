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
using System.Windows.Input;

namespace CookMaster.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        //Injektera _userManager till konstruktorn
        private readonly UserManager _userManager;
        
        //EventHandlers för att kunna stänga fönster med Action event
        public event Action<string>? ConfirmNewUser;
        public event Action RequestClose;

        //Properties
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

        public List<String> Countries { get; set; } = new List<string> { "Sverige", "Finland", "Norge", "Danmark"};
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

        //Button commands som endast sätts en gång via konstruktorn
        public ICommand NewUserCommand { get;}
        public ICommand CancelCommand { get;}
        public RegisterViewModel(UserManager userManager)
        {
            _userManager = userManager;

            NewUserCommand = new RelayCommand(execute => CreateUser(), canExecute => AllInputBoxesChecked());
            CancelCommand = new RelayCommand(execute => Cancel());
        }
        
        private bool AllInputBoxesChecked()
        {
            //Kontrollerar att alla fält är ifyllda för att "Register"-button ska bli enabled
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(ConfirmPwd) && !string.IsNullOrWhiteSpace(SelectedCountry) && !string.IsNullOrWhiteSpace(SelectedQ) && !string.IsNullOrWhiteSpace(SecurityAnswer) && CheckUsername(UserName) && MatchingPwd() && ValidPwd())
            {
                
                return true;
                
            }
            else
            {
                return false;
                
            }
            
        }

        private bool ValidPwd()
        {
            //Kontrollerar att lsenordet hr rätt format
            if (_userManager.ValidatePassword(ConfirmPwd))
            {
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
            //Kontrollerar att lösenorden matchar i båda PasswordBoxarna
            if (Password == ConfirmPwd)
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

        private bool CheckUsername(string username)
        {
            //Kontrollerar ifall användarnamnet är upttaget eller ej
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

        private void CreateUser()
        {
            //Anropar _userManagers metod för att skapa en ny användare och skickar med argumenten i metoden
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
