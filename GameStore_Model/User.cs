using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore_Model
{
	public class User
	{
		public User()
		{
			Games = new List<UserGame>();
		}
		public User(string name, string email, string passwordHash, bool hasAdminRights) : this()
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Email = email ?? throw new ArgumentNullException(nameof(email));
			PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
			HasAdminRights = hasAdminRights;
		}
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string PasswordHash { get; set; }
		public bool HasAdminRights { get; set; }
		public List<UserGame> Games { get; set; }
	}
}
