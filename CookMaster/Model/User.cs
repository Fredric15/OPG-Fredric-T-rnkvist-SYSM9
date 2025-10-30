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
		private string _userName;

		public string UserName
		{
			get { return _userName; }
			set { _userName = value; }
		}
		
		private string _password;
		
		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

        private string _country;
        public string Country
		{
			get { return _country; }
			set { _country = value; }
		}

        private string _securityQuestion;
        public string SecurityQuestion
		{
			get { return _securityQuestion; }
			set { _securityQuestion = value; }
		}
        
		private string _securityAnswer;
        public string SecurityAnswer
		{
			get { return _securityAnswer; }
			set { _securityAnswer = value; }
		}


        public void ValidateLogin()
        {
	
		}

		public void ChangePassword()
		{ }

		public void UpdateUsername(string username)
		{ 
			UserName = username;
		}
        public void UpdatePassword(string password)
        {
            
            Password = password;

        }
		public void UpdateCountry(string country)
		{
			Country = country;
		}
        public void UpdateSecurity(string securityQ, string securityA)
        {
            SecurityQuestion = securityQ;
            SecurityAnswer = securityA;
        }
    }

	
}
