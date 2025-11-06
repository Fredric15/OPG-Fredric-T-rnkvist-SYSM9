using CookMaster.Managers;
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
    public class TwoFactorAuthViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        public event Action<string>? GenerateCode;
        public event Action Confirm;
        public event Action CancelButton;

        private string _userInputCode;
        public string UserInputCode
        {
            get { return _userInputCode; }
            set 
            { 
                _userInputCode = value;
                OnPropertyChanged();
            }
        }
        public ICommand ConfirmCommand { get;}
        public ICommand CancelCommand { get;}
        public ICommand GetCodeCommand { get;}
        
        public TwoFactorAuthViewModel(UserManager userManager)
        {
            _userManager = userManager;

            ConfirmCommand = new RelayCommand(execute => ConfirmCode(UserInputCode));
            CancelCommand = new RelayCommand(execute => Cancel());
            GetCodeCommand = new RelayCommand(execute => GetCode());
            
        }

        private void GetCode()
        {
            //Hämtar koden igen och visar den i en Popup när man trycker på "Get Code"
            GenerateCode?.Invoke($"Verification code: {_userManager.TwoFactorCode}");
        }
        private void Cancel()
        {
            CancelButton?.Invoke();
            
        }

        private void ConfirmCode(string userinput)
        {
            //Kontrollerar om användarens inmatning matchar med den genererade koden
            if (userinput.Equals(_userManager.TwoFactorCode))
            {
                
                Confirm?.Invoke();
            }
            else
            {
                CancelButton.Invoke();
            }
            
        }
    }
}
