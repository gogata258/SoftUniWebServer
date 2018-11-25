using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ByTheCake_Model
{
	public class User
	{
		public User()
		{
			Orders = new List<Order>();
		}
		public User(string fullName, string username, string passwordHash) : base()
		{
			RegistrationDate = DateTime.UtcNow;
			FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
			Username = username ?? throw new ArgumentNullException(nameof(username));
			PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
		}
		public int Id { get; set; }
		[Required, MinLength(3)]
		public string FullName { get; set; }
		[Required, MinLength(3)]
		public string Username { get; set; }
		[Required, StringLength(64, MinimumLength = 64)]
		public string PasswordHash { get; set; }
		[Required]
		public DateTime RegistrationDate { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}
