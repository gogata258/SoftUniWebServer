using System;
using System.Collections.Generic;

namespace ByTheCake_Model
{
	public  class Order
	{
		Order()
		{
			Products = new List<ProductOrder>();
		}
		public int Id { get; set; }
		public DateTime DateOfCreation { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public ICollection<ProductOrder> Products { get; set; }
		public Order(int userId, ICollection<ProductOrder> products)
		{
			UserId = userId;
			DateOfCreation = DateTime.UtcNow;
			Products = products ?? throw new ArgumentNullException(nameof(products));
		}
	}
}