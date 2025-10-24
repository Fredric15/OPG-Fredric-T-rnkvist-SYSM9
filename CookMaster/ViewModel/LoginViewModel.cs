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

		public LoginViewModel(UserManager userManager)
		{
			_userManager = userManager;
			
		}

	
		public RelayCommand LoginCommand => new RelayCommand(execute => Login(), canExecute => CanLogin());
        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public RelayCommand RecoverPwCommand => new RelayCommand(execute => RecoverPassword(), canExecute => ForgotPassword());

        private void RecoverPassword()
        {
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
			RecoverPwWindow recover = new RecoverPwWindow();
            recover.DataContext = new RecoverPwViewModel(userManager);
            recover.ShowDialog();
        }

        private bool CanLogin() => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
		private void Login()
		{
			if (_userManager.Login(Username,Password))
			{
				RegisterWindow rw = new RegisterWindow();
				rw.Show(); 
				
			}
			else
			{ 
				ErrorText = "Fel användarnamn eller lösenord."; 
			}
		}
		private void Register()
		{
			var userManager = (UserManager)Application.Current.Resources["UserManager"];
			RegisterWindow rw = new RegisterWindow();
			rw.DataContext = new RegisterViewModel(userManager);
			rw.ShowDialog();
		}

		private bool ForgotPassword()
		{
			if (ErrorText != null)
			{
				return true;
			}
			else
			{  return false; }
		}

    }
}
