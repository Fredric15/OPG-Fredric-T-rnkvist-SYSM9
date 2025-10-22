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
		private ImageSource _displayLogo;
		
		public ImageSource DisplayLogo
		{
			get { return _displayLogo; }
			set
			{
				_displayLogo = value;
				OnPropertyChanged();
			}
		}
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

        public LoginViewModel(UserManager userManager)
		{
			
			_userManager = userManager;
			
			//Skickar bild till metod
			LoadImageFromResource("C:\\Users\\fredr\\source\\repos\\Sysm9\\Inlämningsuppgift\\CookMaster\\CookMaster\\MVVM\\marca.png");
		}

	
		public RelayCommand LoginCommand => new RelayCommand(execute => Login(), canExecute => CanLogin());
        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public RelayCommand ForgotPCommand => new RelayCommand(execute => Login(), canExecute => CanLogin());


        private bool CanLogin() => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
		private void Login()
		{
			if (_userManager.Login(Username,Password))
			{
				RegisterWindow rw = new RegisterWindow();
				
			}
		}
		private void Register()
		{
            RegisterWindow rw = new RegisterWindow();
			rw.DataContext = new RegisterViewModel(_userManager);
			rw.Show();
			
			
			
			

        }
        private void LoadImageFromResource(string v)
        {
			try
			{
				var uri = new Uri(v);
				var bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.UriSource = uri;
				DisplayLogo = bitmap;
			}
			catch (Exception ex)
			{
				DisplayLogo = null;

			}
        }
    }
}
