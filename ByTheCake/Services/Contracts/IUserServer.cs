namespace ByTheCake_App.Services.Contracts
{
	using ViewModel.Account;
	public interface IUserService
	{
		void Register(RegisterUserViewModel user);
		bool Login(LoginUserViewModel user);
		int GetUserId(string username);
		AccountDetailsViewModel GetUserProfile(int id);
	}
}
