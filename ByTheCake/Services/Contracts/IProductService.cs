using System.Collections.Generic;

namespace ByTheCake_App.Services.Contracts
{
	using global::ByTheCake_App.ViewModel.Product;

	public interface IProductService
	{
		bool Add(ProductViewModel product);
		ICollection<ProductSearchItemViewModel> GetProducts(string query);
		ProductViewModel Find(int id);
	}
}
