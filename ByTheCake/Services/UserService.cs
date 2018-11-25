using ByTheCake_DAL;
using System;
using System.Linq;

namespace ByTheCake_App.Services
{
	using Contracts;
	using ViewModel.Account;
	public class UserService : IUserService
	{
		public async void Register(RegisterUserViewModel user)
		{
			using (Context dc = new Context())
			{
				await dc.Users.AddAsync(new ByTheCake_Model.User(user.FullName, user.Username, CustomHashAlgorithm.HashNew(user.Password)));
				await dc.SaveChangesAsync();
			}
		}
		public AccountDetailsViewModel GetUserProfile(int id)
		{
			using (Context dc = new Context())
			{
				ByTheCake_Model.User user = dc.Users.FirstOrDefault(x => x.Id == id);
				if (user is null) throw new ArgumentException("id is invalid");
				else return new AccountDetailsViewModel(user.Username, user.RegistrationDate, dc.Users.Where(u => u.Id == id).Select(x => x.Orders.Count()).First());
			}
		}
		public bool Login(LoginUserViewModel user)
		{
			if (Exists(user))
				using (Context dc = new Context())
					if (dc.Users.Any(x => x.Username == user.Username && CustomHashAlgorithm.CheckHash(user.Pasword, x.PasswordHash))) return true;
			return false;
		}
		public int GetUserId(string username)
		{
			if (Exists(username))
				using (Context dc = new Context())
					return dc.Users.First(x => x.Username == username).Id;
			else throw new ArgumentException("username does not exists");
		}
		bool Exists(RegisterUserViewModel user) => Exists(user.Username);
		bool Exists(LoginUserViewModel user) => Exists(user.Username);
		bool Exists(string user)
		{
			using (Context dc = new Context())
			{
				if (dc.Users.FirstOrDefault(x => x.Username == user) is null) return false;
				else return true;
			}
		}
		bool Create(RegisterUserViewModel user)
		{
			if (user.ConfimPassword == user.Password && !Exists(user))
			{
				Register(user);
				return true;
			}
			return false;
		}
	}
}
