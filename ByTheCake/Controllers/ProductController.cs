using System.Collections.Generic;
using System.Linq;

namespace ByTheCake_App.Controllers
{
	using Server.Abstractions;
	using Server.HTTP.Contracts;
	using Services;
	using Services.Contracts;
	using ViewModel;
	using ViewModel.Product;

	public class ProductController : ControllerBase
	{
		readonly IProductService service;
		private const string AddView = @"Product\Add";
		private const string SearchView = @"Product\Search";
		private const string DetailsView = @"Product\Details";
		protected override string ApplicationDirectory => System.Environment.CurrentDirectory;
		public ProductController()
		{
			service = new ProductService();
		}
		internal IHttpResponse AddGet()
		{
			ViewData["showResult"] = "none";
			return FileViewResponse(AddView);
		}
		internal IHttpResponse AddPost(IHttpRequest request)
		{
			ErrorCheckData(request, "name");
			ErrorCheckData(request, "price");
			ErrorCheckData(request, "url");

			ProductViewModel product = new ProductViewModel(request.FormData["name"], decimal.Parse(request.FormData["price"]), request.FormData["url"]);
			service.Add(product);

			ViewData["name"] = product.Name;
			ViewData["price"] = product.Price.ToString();
			ViewData["url"] = product.Url;
			return FileViewResponse(AddView);
		}
		internal IHttpResponse SearchGet(IHttpRequest request) => SearchGet(request, "");
		internal IHttpResponse SearchPost(IHttpRequest request)
		{
			string query = request.FormData["query"] ?? "";
			return SearchGet(request, query);
		}
		internal IHttpResponse SearchGet(IHttpRequest request, string query)
		{
			string results = string.Empty;
			int itemsCount = request.Session.Get<Cart>(Cart.CartSessionKey).ProductIds.Count;
			ICollection<ProductSearchItemViewModel> foundProduct = service.GetProducts(query);
			if (!foundProduct.Any()) ViewData["results"] = "<h4>No resuts found</h4>";
			else foundProduct.ToList().ForEach(x => results += ItemEntryHtml(x));

			ViewData["results"] = results;
			ViewData["itemCount"] = itemsCount.ToString();
			return FileViewResponse(SearchView);
		}
		internal IHttpResponse DetailsGet(IHttpRequest request)
		{
			int id = int.Parse(request.UrlParameters["id"]);
			ProductViewModel found = service.Find(id);

			ViewData["name"] = found.Name;
			ViewData["price"] = found.Price.ToString();
			ViewData["url"] = found.Url;

			return FileViewResponse(DetailsView);
		}

		string ItemEntryHtml(ProductSearchItemViewModel product)
		{
			return $"" +
			$"<div>" +
				$@"<a href=""/product/{product.Id}"">{product.Name}</a> - ${product.Price:F2} " +
				@"<form method=""post"" action=""/addToCart"">" +
						$@"<input type=""hidden"" value=""{product.Id}"" name=""id""/>" +
						@"<input type=""submit"" value=""Order""/>" +
				"</form>" +
			"</div>" +
			"</br>";
		}
	}
}
