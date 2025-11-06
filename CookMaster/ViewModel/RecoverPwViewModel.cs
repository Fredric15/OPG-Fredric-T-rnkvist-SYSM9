using CookMaster.Managers;
using Microsoft.Win32;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CookMaster.ViewModel
{
    public class RecoverPwViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        public event Action<string>? RequestMessage;
        public event Action? RequestClose;

        private string inputUsername;
        public string InputUsername
        {
            get { return inputUsername; }
            set 
            { 
                inputUsername = value;
                OnPropertyChanged();
            }
        }
        private string _inputsecurityanswer;
        public string InputSecurityAnswer
        {
            get { return _inputsecurityanswer; }
            set 
            { 
                _inputsecurityanswer = value; 
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
        private bool _sequritymatch = false;
        public bool SequrityMatch
        {
            get { return _sequritymatch; }
            private set
            {
                _sequritymatch = value;
                OnPropertyChanged();
            }
        }
        private string _newPwd;
        public string NewPwd
        {
            get { return _newPwd; }
            set 
            { 
                _newPwd = value; 
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
        private string _errorPwd;

        public string ErrorPwd
        {
            get { return _errorPwd; }
            set 
            { 
                _errorPwd = value;
                OnPropertyChanged();
            }
        }
        public ICommand FindUserCommand { get; }
        public ICommand ConfirmAnswerCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public RecoverPwViewModel(UserManager usermanager)
        {
            _userManager = usermanager;
            FindUserCommand = new RelayCommand(execute => FindUser(), canExecute => CanFindUser());
            ConfirmAnswerCommand = new RelayCommand(execute => ConfirmAnswer(), canExecute => CanConfirmAnswer());
            SaveCommand = new RelayCommand(execute => Save(), canExecute => canSave());
            CancelCommand = new RelayCommand(execute => Cancel());
        }

        private void Cancel()
        {
            _userManager.Logout();
            RequestClose?.Invoke();
        }

        private bool canSave()
        {
            //Ser till så att man endast kan trycka Save om båda password-fälten är ifyllda samt att lösenorden matchar
            
            if (!string.IsNullOrWhiteSpace(NewPwd) && !string.IsNullOrWhiteSpace(ConfirmPwd) && NewPwd == ConfirmPwd)
            {
                return true;
            }
            return false;
        }

        private void Save()
        {
            //Anropar _userManagers metod för att validera formatet på lösenordet
            //Om true så anroper den _usermanagers ChangePassword-metod
            
            if (_userManager.ValidatePassword(ConfirmPwd))
            {
                _userManager.ChangePassword(InputUsername, NewPwd);
                RequestClose?.Invoke();
                RequestMessage?.Invoke("Ditt lösenord har ändrats!");

            }
            else
            {
                ErrorPwd = "Lösenordet måste vara minst 8 tecken, samt innehålla en siffra och ett specialtecken.";
            }
        }
        private void FindUser()
        {
            //Söker efter inmatade användarnamnet
             
            if (_userManager.FindUser(InputUsername))
            {
                ErrorText = string.Empty;
            }
            else
            {
                ErrorText = "Ingen användare hittades.";
            }

        }
        private bool CanConfirmAnswer()
        {
            //Så länge textrutan för Säkerhetssvaret är tom och ingen användare har hittats så är knappen inaktiv

            if (!string.IsNullOrWhiteSpace(InputSecurityAnswer) && _userManager.CurrentUser != null)
            {
                return true;
            }
            return false;
        }

        private void ConfirmAnswer()
        {
            //Kontrollerar om inmatade svaret stämmer överrens med CurrentUser sparade svar, samt ingnorar eventuell storbokstav 
            if (string.Equals(_userManager.CurrentUser.SecurityAnswer, InputSecurityAnswer, StringComparison.OrdinalIgnoreCase))
            {
                ErrorText = "Ditt svar matchade.";
                SequrityMatch = true;
                OnPropertyChanged();
            }
            else
            {
                SequrityMatch = false; 
                OnPropertyChanged();
                ErrorText = "Ditt svar matchade ej.";
            }  
        }
        private bool CanFindUser()
        {
            if (!string.IsNullOrWhiteSpace(InputUsername))
            {
                return true;
            }
            return false;


        
        }
        

    }
}
