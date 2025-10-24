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
        private string _newpassword;
        public string NewPw
        {
            get { return _newpassword; }
            set 
            { 
                _newpassword = value; 
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
            
            RequestClose?.Invoke();
        }

        private bool canSave()
        {
            if (!string.IsNullOrWhiteSpace(NewPw) && !string.IsNullOrWhiteSpace(ConfirmPw) && NewPw == ConfirmPw)
            {
                return true;
            }
            return false;
        }

        private void Save()
        {
            if (_userManager.ValidatePassword(ConfirmPw))
            {
                _userManager.ChangePassword(InputUsername, NewPw);
                RequestClose?.Invoke();
                RequestMessage?.Invoke("Ditt lösenord har ändrats!");

            }
            else
            {
                ErrorText = "Ej korrekt format på pw";
            }
            
            
        }

        private bool CanConfirmAnswer()
        {
            if (!string.IsNullOrWhiteSpace(InputSecurityAnswer))
            {
                return true;
            }
            return false;
        }

        private void ConfirmAnswer()
        {
            
            if (string.Equals(_userManager.CurrentUser.SecurityAnswer, InputSecurityAnswer, StringComparison.OrdinalIgnoreCase))
            {
                ErrorText = "Ditt svar matchade.";
                SequrityMatch = true;
                OnPropertyChanged();
            }
            else
            {
                ErrorText = "Ditt svar matchade ej.";
            }  
        }

        public ICommand FindUserCommand { get;}
        public ICommand ConfirmAnswerCommand { get;}
        public ICommand SaveCommand { get;}
        public ICommand CancelCommand { get;}

        private bool CanFindUser()
        {
            if (!string.IsNullOrWhiteSpace(InputUsername))
            {
                return true;
            }
            return false;


        
        }
        private void FindUser()
        {
            if (_userManager.FindUser(InputUsername))
            {
               ErrorText = string.Empty;
            }
            else
            {
                ErrorText = "Ingen användare hittades.";
            }
        
        }

    }
}
