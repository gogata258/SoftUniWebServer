using System;

namespace ByTheCake_App.ViewModel.Product
{
	public class ProductViewModel
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Url { get; set; }
		public ProductViewModel(string name, decimal price, string url)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Price = price;
			Url = url ?? throw new ArgumentNullException(nameof(url));
		}
	}
}
