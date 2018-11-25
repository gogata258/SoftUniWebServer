using System;
using System.Collections.Generic;

namespace ByTheCake_App.Controllers
{
	using Server.Abstractions;
	using Server.HTTP;
	using Server.HTTP.Contracts;
	using Services;
	using Services.Contracts;
	using ViewModel;
	using ViewModel.Account;
	using ViewModel.Product;

	public class ShoppingController : ControllerBase
	{
		private const string OrderSuccessView = @"Shopping\OrderSuccess";
		private const string CartView = @"Shopping\Cart";
		private const string OrdersView = @"Shopping\Orders";
		private const string OrderDetailsView = @"Shopping\OrderDetails";

		readonly IShoppingService service;
		public ShoppingController()
		{
			service = new ShoppingService();
		}
		protected override string ApplicationDirectory => System.Environment.CurrentDirectory;
		//TODO: make it pull the items correctly
		internal IHttpResponse OrderGet(IHttpRequest request)
		{
			Cart currentCart = request.Session.Get<Cart>(Cart.CartSessionKey);
			int userId = request.Session.Get<int>(SessionStore.SessionLoginId);
			service.Order(currentCart, userId);
			request.Session.Add(Cart.CartSessionKey, new Cart());
			return FileViewResponse(OrderSuccessView);
		}
		internal IHttpResponse AddToCartPost(IHttpRequest request)
		{
			ErrorCheckData(request, "id");
			int id = int.Parse(request.FormData["id"]);
			request.Session.Get<Cart>(Cart.CartSessionKey).Add(id);
			return RedirectResponse("/search");
		}
		internal IHttpResponse GetOrders(IHttpRequest request)
		{
			string tableContent = string.Empty;
			ICollection<OrderViewModel> orders = service.GetOrders(request.Session.Get<int>(SessionStore.SessionLoginId));
			foreach (var item in orders) tableContent += TableOrderRowHtml(item);

			ViewData["table"] = tableContent;
			return FileViewResponse(OrdersView);
		}
		internal IHttpResponse GetOrderDetails(IHttpRequest request)
		{
			int id = int.Parse(request.UrlParameters["id"]);
			DateTime creationDate;

			string result = string.Empty;
			foreach (var item in service.GetProductInOrder(id, out creationDate))
				result += TableOrderDetailsRowHtml(item);

			ViewData["table"] = result;
			ViewData["creationDate"] = creationDate.ToLongDateString();
			ViewData["id"] = id.ToString();
			return FileViewResponse(OrderDetailsView);
		}
		internal IHttpResponse CartGet(IHttpRequest request)
		{
			string result = string.Empty;
			decimal totalCost = 0;

			Cart cart = request.Session.Get<Cart>(Cart.CartSessionKey);
			List<ProductViewModel> products = service.GetProductsInCart(cart);

			foreach (var item in products)
			{
				result += GetHtmlForProduct(item);
				totalCost += item.Price;
			}

			ViewData["cartItems"] = result;
			ViewData["totalCost"] = $"{totalCost:f2}$";
			return FileViewResponse(CartView);
		}
		private string TableOrderRowHtml(OrderViewModel item)
		{
			return "" +
				"<tr>" +
					$"<td><a href='/orderDetails/{item.Id}'>{item.Id}<a></td>" +
					$"<td>{item.CreateOn.ToLongDateString()}</td>" +
					$"<td>{item.Total}</td>" +
				"</tr>";
		}
		private string TableOrderDetailsRowHtml(ProductSearchItemViewModel item)
		{
			return "" +
				"<tr>" +
					$"<td>{item.Name}</td>" +
					$"<td>{item.Price:f2}</td>" +
				"</tr>";
		}
		string GetHtmlForProduct(ProductViewModel product)
		{
			return
				$"<div>" +
					$"<p>{product.Name} - {product.Price}$</p>" +
				$"</div>";
		}
	}
}
