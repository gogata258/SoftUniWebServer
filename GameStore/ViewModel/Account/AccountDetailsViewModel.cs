using System;

namespace GameStore_App.ViewModel.Account
{
	public class AccountDetailsViewModel
	{
		public string Email { get; set; }
		public bool IsAdmin { get; set; }
		public AccountDetailsViewModel(string username, bool isAdmin)
		{
			Email = username ?? throw new ArgumentNullException(nameof(username));
			IsAdmin = isAdmin;
		}
	}
}
