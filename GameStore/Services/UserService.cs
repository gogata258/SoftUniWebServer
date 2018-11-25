using GameStore_DAL;
using GameStore_Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore_App.Services
{
	using Contracts;
	using ViewModel.Account;

	public class UserService : IUserService
	{
		public int GetUserId(string mail)
		{
			if (Exists(mail))
				using (Context dc = new Context())
					return dc.Users.First(x => x.Email == mail).Id;
			else throw new ArgumentException("username does not exists");
		}

		public AccountDetailsViewModel GetUserProfile(int id)
		{
			using (Context dc = new Context())
			{
				User user = dc.Users.FirstOrDefault(x => x.Id == id);
				if (user is null) throw new ArgumentException("id is invalid");
				else return new AccountDetailsViewModel(user.Email, user.HasAdminRights);
			}
		}

		public bool Login(LoginUserViewModel user)
		{
			if (Exists(user))
				using (Context dc = new Context())
					if (dc.Users.Any(x => x.Email == user.Email && CustomHashAlgorithm.CheckHash(user.Pasword, x.PasswordHash))) return true;
			return false;
		}

		bool Exists(RegisterUserViewModel user) => Exists(user.Email);
		bool Exists(LoginUserViewModel user) => Exists(user.Email);
		bool Exists(string mail)
		{
			using (Context dc = new Context())
			{
				if (dc.Users.FirstOrDefault(x => x.Email == mail) is null) return false;
				else return true;
			}
		}
		public async Task<bool> Register(RegisterUserViewModel user)
		{
			using (Context dc = new Context())
			{
				if (!Exists(user))
				{
					await dc.Users.AddAsync(new User(user.FullName, user.Email, CustomHashAlgorithm.HashNew(user.Password), IsFirstUser()));
					await dc.SaveChangesAsync();
					return true;
				}
				else return false;
			}
		}

		private bool IsFirstUser()
		{
			using (Context db = new Context()) return (db.Users.Count() == 0) ? true : false;
		}

		public bool CheckAdminStatus(int id)
		{
			using (Context db = new Context())
				return db.Users.Find(id).HasAdminRights;
		}
	}
}
