using System;

namespace ByTheCake_App.ViewModel.Account
{
	public class LoginUserViewModel
	{
		public const string SessionLoginId = "USER_ID";
		public string Username { get; set; }
		public string Pasword { get; set; }
		public LoginUserViewModel(string username, string password)
		{
			Username = username ?? throw new ArgumentNullException(nameof(username));
			Pasword = password ?? throw new ArgumentNullException(nameof(password));
		}
		public LoginUserViewModel(RegisterUserViewModel user) : this(user.Username, user.Password)
		{

		}
	}
}
