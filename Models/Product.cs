using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ByTheCake_Model
{
	public  class Product
	{
		Product()
		{
			Orders = new List<ProductOrder>();
		}
		public int Id { get; set; }
		[Required, MinLength(3)]
		public string Name { get; set; }
		[Required, DataType(DataType.Currency)]
		public decimal Price { get; set; }
		[Required]
		public string ImageUrl { get; set; }
		public ICollection<ProductOrder> Orders { get; set; }
		public Product(string name, decimal price, string imageUrl) : this()
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Price = price;
			ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
		}
	}
}