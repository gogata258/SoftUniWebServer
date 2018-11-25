using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ByTheCake_DAL;

namespace ByTheCake_App
{
	using Controllers;
	using Server.Abstractions;
	using Server.Routing.Contracts;
	using ViewModel.Product;

	public class ByTheCake : ApplicationBase
	{
		public static List<ProductViewModel> CakeList = new List<ProductViewModel>();
		public override void Configure(IAppRouteConfig routeConfig)
		{
			InitilizeDatabase();
			SetAnonimousPaths(routeConfig);
			SetRouting(routeConfig);
		}
		protected override void InitilizeDatabase()
		{
			using (Context dbc = new Context()) dbc.Database.Migrate();
		}
		protected override void SetAnonimousPaths(IAppRouteConfig routeConfig)
		{
			routeConfig.AnonymousPaths.Add("/login");
			routeConfig.AnonymousPaths.Add("/register");
		}
		protected override void SetRouting(IAppRouteConfig routeConfig)
		{
			routeConfig.AddGet("/", context => new HomeController().Index());
			routeConfig.AddGet("/about", context => new HomeController().About());

			routeConfig.AddGet("/register", context => new AccoutController().RegisterGet(context));
			routeConfig.AddPost("/register", context => new AccoutController().RegisterPost(context));
			routeConfig.AddGet("/login", context => new AccoutController().LoginGet(context));
			routeConfig.AddPost("/login", context => new AccoutController().LoginPost(context));
			routeConfig.AddGet("/logout", context => new AccoutController().LogoutGet(context));
			routeConfig.AddGet("/profile", context => new AccoutController().ProfileGet(context));

			routeConfig.AddGet("/add", context => new ProductController().AddGet());
			routeConfig.AddPost("/add", context => new ProductController().AddPost(context));
			routeConfig.AddPost("/search", context => new ProductController().SearchPost(context));
			routeConfig.AddGet("/search", context => new ProductController().SearchGet(context));
			routeConfig.AddGet("/product/{(?<id>[0-9]+)}", context => new ProductController().DetailsGet(context));

			routeConfig.AddPost("/addToCart", context => new ShoppingController().AddToCartPost(context));
			routeConfig.AddGet("/cart", context => new ShoppingController().CartGet(context));
			routeConfig.AddGet("/success", context => new ShoppingController().OrderGet(context));
			routeConfig.AddGet("/orders", context => new ShoppingController().GetOrders(context));
			routeConfig.AddGet("/orderDetails/{(?<id>[0-9]+)}", context => new ShoppingController().GetOrderDetails(context));
		}
	}
}
