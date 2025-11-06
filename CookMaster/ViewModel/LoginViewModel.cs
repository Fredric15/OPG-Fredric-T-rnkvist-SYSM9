using CookMaster.Managers;
using CookMaster.Model;
using CookMaster.Views;
using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CookMaster.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
		private readonly UserManager _userManager;
	
		private string _username;

		public string Username
		{
			get { return _username; }
			set { _username = value;
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
		public ICommand LoginCommand { get;}
		public ICommand RegisterCommand { get;}
		public ICommand RecoverPwCommand { get;}
        
		//Events som fönstrets code-behind kan prenumerera på och utföra olika handlingar
		public event EventHandler<string> OnLoginSuccess;
        public event EventHandler OpenRegister;
        public event EventHandler RecoverPwd;
        
		public LoginViewModel(UserManager userManager)
		{
            _userManager = userManager;

            LoginCommand = new RelayCommand(execute => Login(), canExecute => CanLogin());
            RegisterCommand = new RelayCommand(execute => Register());
            RecoverPwCommand = new RelayCommand(execute => RecoverPassword());
        }

		private void RecoverPassword()
        {
            RecoverPwd?.Invoke(this, EventArgs.Empty);
		}

		//En metod som gör Login-button enabled så länge Username och Password innehåller text 
        private bool CanLogin() => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
		private void Login()
		{
			//_userManager anropar sin Login-metod och skickar med användarens inmatade username samt lösenord
			//Om true så skapas det en sex-siffrig kod som ska matas in i nästa fönster
            Random rnd = new Random();
            
			if (_userManager.Login(Username,Password))
			{
                _userManager.TwoFactorCode = $"{rnd.Next(100000, 999999)}";
                OnLoginSuccess?.Invoke(this, _userManager.TwoFactorCode);
			}
			else
			{
				
				ErrorText = "Fel användarnamn eller lösenord."; 
			}
		}
		private void Register()
		{
			OpenRegister?.Invoke(this, EventArgs.Empty);
		}
	}
}
