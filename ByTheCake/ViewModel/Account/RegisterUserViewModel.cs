using System;

namespace ByTheCake_App.ViewModel.Account
{
	public class RegisterUserViewModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string ConfimPassword { get; set; }
		public string FullName { get; set; }
		public RegisterUserViewModel(string username, string password, string confimPassword, string fullName)
		{
			Username = username ?? throw new ArgumentNullException(nameof(username));
			Password = password ?? throw new ArgumentNullException(nameof(password));
			ConfimPassword = confimPassword ?? throw new ArgumentNullException(nameof(confimPassword));
			FullName = fullName ?? string.Empty;
		}
	}
}
