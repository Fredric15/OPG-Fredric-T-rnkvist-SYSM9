using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CookMaster.Model
{
    public class User
    {
		private string userName;

		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}
		
		private string password;
		
		public string Password
		{
			get { return password; }
			set { password = value; }
		}

        private string country;

		public string Country
		{
			get { return country; }
			set { country = value; }
		}

		public string SecurityQuestion { get; set; }

		public string SecurityAnswer { get; set; }


        public void ValidateLogin()
        {
	
		}

		public void ChangePassword()
		{ }

		public void UpdateDetails()
		{ }
    }

	
}
