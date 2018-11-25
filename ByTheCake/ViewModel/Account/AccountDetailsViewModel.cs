using System;

namespace ByTheCake_App.ViewModel.Account
{
	public class AccountDetailsViewModel
	{
		public string Username { get; set; }
		public DateTime RegistrationDate { get; set; }
		public int Count { get; set; }
		public AccountDetailsViewModel(string username, DateTime registrationDate, int count)
		{
			Username = username ?? throw new ArgumentNullException(nameof(username));
			RegistrationDate = registrationDate;
			Count = count;
		}
	}
}
