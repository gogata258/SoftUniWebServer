using Server.HTTP;
using Server.HTTP.Contracts;
using System;

namespace GameStore_App.Controllers
{
	using GameStore_App.ViewModel.Game;
	using Services;
	using Services.Contracts;
	using ViewModel;
	public class ShoppingController : BaseController
	{
		private const string CartView = @"Shopping/Cart";
		private const string OrderSuccessView = @"Shopping/OrderSuccess";
		readonly IShoppingService service;
		public ShoppingController(IHttpRequest request) : base(request)
		{
			service = new ShoppingService();
		}
		internal IHttpResponse OrderSucces()
		{
			return FileViewResponse(OrderSuccessView);
		}
		internal IHttpResponse AddToCartGet(IHttpRequest context)
		{
			int gameId = int.Parse(context.UrlParameters["id"]);
			if(Authentication.IsAuthenticated && !service.CheckOwned(gameId, context.Session.Get<int>(SessionStore.SessionLoginId)))
				context.Session.Get<Cart>(Cart.CartSessionKey).Add(gameId);
			else if(!Authentication.IsAuthenticated) context.Session.Get<Cart>(Cart.CartSessionKey).Add(gameId);
			return RedirectResponse("/");
		}
		internal IHttpResponse CheckoutGet(IHttpRequest context)
		{
			string result = string.Empty;
			decimal totalPrice = 0;
			foreach (var id in context.Session.Get<Cart>(Cart.CartSessionKey).ProductIds)
			{
				result += GetCartItemHtml(service.Get(id));
				totalPrice += service.GetPrice(id);
			}
			ViewData["cart"] = result;
			ViewData["priceTotal"] = totalPrice.ToString();
			return FileViewResponse(CartView);
		}
		internal IHttpResponse CheckoutPost(IHttpRequest request)
		{
			if (!Authentication.IsAuthenticated)
				if(request.FormData.Count == 0) return new UserController(request).LoginGet(request);
				else return new UserController(request).LoginPost(request, true);
			
			service.Purchase(request.Session.Get<Cart>(Cart.CartSessionKey), request.Session.Get<int>(SessionStore.SessionLoginId));
			return RedirectResponse("/orderSuccess");
			
		}
		string GetCartItemHtml(CartViewGameItemViewModel game)
		{
			return "" +
				"<div class='list-group-item'>\n" +
					"<div class='media'>\n" +
						$"<a class='btn btn-outline-danger btn-lg align-self-center mr-3' href='/removeFromCart/{game.Id}'>X</a>\n" +
						$"<img class='d-flex mr-4 align-self-center img-thumbnail' height='127' src='{game.Cover}' width='227' alt='cover image'>\n" +
						"<div class='media-body align-self-center'>\n" +
							$"<a href='#'><h4 class='mb-1 list-group-item-heading'>{game.Title}</h4></a>\n" +
							$"<p>{game.DescriptionShort}</p>\n" +
						"</div>\n" +
						"<div class='col-md-2 text-center align-self-center mr-auto'>\n" +
							$"<h2> {game.Price}&euro; </h2>\n" +
						"</div>\n" +
					"</div>\n" +
				"</div>\n";
		}

		internal IHttpResponse RemoveFromCartGet(IHttpRequest context)
		{
			Cart cart = context.Session.Get<Cart>(Cart.CartSessionKey);
			int id = int.Parse(context.UrlParameters["id"]);
			if (cart.ProductIds.Contains(id)) cart.ProductIds.Remove(id);
			return RedirectResponse("/checkout");
		}
	}
}
