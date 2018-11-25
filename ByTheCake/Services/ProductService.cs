using ByTheCake_DAL;
using System.Collections.Generic;
using System.Linq;

namespace ByTheCake_App.Services
{
	using Services.Contracts;
	using ViewModel.Product;
	using ByTheCake_Model;
	public class ProductService : IProductService
	{
		public bool Add(ProductViewModel product)
		{
			if (!Exists(product))
			{
				Create(product);
				return true;
			}
			else return false;
		}
		public ICollection<ProductSearchItemViewModel> GetProducts(string query)
		{
			using (Context dc = new Context())
				return dc.Products.Where(x => x.Name.Contains(query))
					.Select(x => new ProductSearchItemViewModel(x.Name, x.Price, x.Id)).ToList();
		}
		public ProductViewModel Find(int id)
		{
			using (Context dc = new Context())
			{
				Product product = dc.Products.Find(id);
				return new ProductViewModel(product.Name, product.Price, product.ImageUrl);
			}
		}
		bool Exists(ProductViewModel model)
		{
			using (Context dc = new Context())
				if (dc.Products.Any(x => x.Name == model.Name)) return true;
			return false;
		}
		async void Create(ProductViewModel product)
		{
			using (Context dc = new Context())
			{
				await dc.Products.AddAsync(new Product(product.Name, product.Price, product.Url));
				await dc.SaveChangesAsync();
			}
		}
	}
}
