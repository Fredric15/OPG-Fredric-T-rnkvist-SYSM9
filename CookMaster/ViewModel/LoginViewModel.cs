using MVVM_KlonaMIg.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CookMaster.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
		private ImageSource _displayLogo;

		public ImageSource DisplayLogo
		{
			get { return _displayLogo; }
			set
			{
				_displayLogo = value;
				OnPropertyChanged(nameof(DisplayLogo));
			}
		}

		public LoginViewModel()
		{
			LoadImageFromResource("C:\\Users\\fredr\\source\\repos\\Sysm9\\Inlämningsuppgift\\CookMaster\\CookMaster\\MVVM\\marca.png");
			
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
