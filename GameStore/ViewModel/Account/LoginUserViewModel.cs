using System;

namespace GameStore_App.ViewModel.Account
{
	public class LoginUserViewModel
	{
		public string Email { get; set; }
		public string Pasword { get; set; }
		public bool IsAdmin { get; set; }
		public LoginUserViewModel(string email, string password)
		{
			Email = email ?? throw new ArgumentNullException(nameof(email));
			Pasword = password ?? throw new ArgumentNullException(nameof(password));
		}
		public LoginUserViewModel(RegisterUserViewModel user) : this(user.Email, user.Password)
		{

		}
	}
}
