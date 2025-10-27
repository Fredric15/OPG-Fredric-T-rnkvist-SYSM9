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
        Random rnd;
        private readonly UserManager _userManager;
        public event Action<string>? GenerateCode;
        public event Action Confirm;
        public event Action CancelButton;
        public int[] sixdigitcode { get; }
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
        public ICommand NewCodeCommand { get;}
        
        public string code { get;}
        public TwoFactorAuthViewModel(UserManager userManager)
        {
            _userManager = userManager;
            rnd = new Random();
            code = $"{rnd.Next(100000, 999999)}";
            

            ConfirmCommand = new RelayCommand(execute => ConfirmCode(UserInputCode));
            CancelCommand = new RelayCommand(execute => Cancel());
            NewCodeCommand = new RelayCommand(execute => NewCode());
            
        }

        private void NewCode()
        {
            GenerateCode?.Invoke($"Verification code: {_userManager.TwoFactorCode}");
        }
        private void Cancel()
        {
            CancelButton?.Invoke();
            
        }

        private void ConfirmCode(string userinput)
        {
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
