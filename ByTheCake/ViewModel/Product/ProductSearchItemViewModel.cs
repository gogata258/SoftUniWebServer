using System;

namespace ByTheCake_App.ViewModel.Product
{
	public class ProductSearchItemViewModel
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Id { get; set; }

		public ProductSearchItemViewModel(string name, decimal price, int id)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Price = price;
			Id = id;
		}
	}
}
