namespace GameStore_App.Services.Contracts
{
	using System.Threading.Tasks;
	using ViewModel.Account;
	public interface IUserService
	{
		Task<bool> Register(RegisterUserViewModel user);
		bool Login(LoginUserViewModel user);
		int GetUserId(string username);
		AccountDetailsViewModel GetUserProfile(int id);
		bool CheckAdminStatus(int id);
	}
}
