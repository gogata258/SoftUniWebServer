using System;

namespace GameStore_App.ViewModel.Account
{
	public class RegisterUserViewModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfimPassword { get; set; }
		public string FullName { get; set; }
		public bool IsAdmin { get; set; }
		public RegisterUserViewModel(string email, string password, string confimPassword, string fullName)
		{
			Email = email ?? throw new ArgumentNullException(nameof(email));
			Password = password ?? throw new ArgumentNullException(nameof(password));
			ConfimPassword = confimPassword ?? throw new ArgumentNullException(nameof(confimPassword));
			FullName = fullName ?? string.Empty;
		}
	}
}
