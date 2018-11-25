namespace GameStore_App
{
	using Controllers;
	using Server.Abstractions;
	using Server.Routing.Contracts;
	public class GameStoreApp : ApplicationBase
	{
		public override void Configure(IAppRouteConfig routeConfig)
		{
			SetAnonimousPaths(routeConfig);
			SetRouting(routeConfig);
		}
		protected override void SetAnonimousPaths(IAppRouteConfig routeConfig)
		{
			routeConfig.AnonymousPaths.Add("/login");
			routeConfig.AnonymousPaths.Add("/register");
			routeConfig.AnonymousPaths.Add("/");
			routeConfig.AnonymousPaths.Add("/gameDatails/{(?<id>[0-9]+)}");
			routeConfig.AnonymousPaths.Add("/addToCart/{(?<id>[0-9]+)}");
			routeConfig.AnonymousPaths.Add("/removeFromCart/{(?<id>[0-9]+)}");
			routeConfig.AnonymousPaths.Add("/checkout");
		}
		protected override void SetRouting(IAppRouteConfig routeConfig)
		{
			routeConfig.AddGet("/", context => new HomeController(context).Home(context));

			routeConfig.AddGet("/register", context => new UserController(context).RegisterGet(context));
			routeConfig.AddPost("/register", context => new UserController(context).RegisterPost(context));
			routeConfig.AddGet("/login", context => new UserController(context).LoginGet(context));
			routeConfig.AddPost("/login", context => new UserController(context).LoginPost(context));
			routeConfig.AddGet("/logout", context => new UserController(context).LogoutGet(context));

			routeConfig.AddGet("/addGame", context => new GameController(context).AddGamesGet());
			routeConfig.AddPost("/addGame", context => new GameController(context).AddGamesPost(context));
			routeConfig.AddGet("/allGames", context => new GameController(context).ViewAllGamesGet(context));
			routeConfig.AddGet("/deleteGame/{(?<id>[0-9]+)}", context => new GameController(context).DeleteGameGet(context));
			routeConfig.AddPost("/deleteGame/{(?<id>[0-9]+)}", context => new GameController(context).DeleteGamePost(context));
			routeConfig.AddGet("/editGame/{(?<id>[0-9]+)}", context => new GameController(context).EditGamesGet(context));
			routeConfig.AddPost("/editGame/{(?<id>[0-9]+)}", context => new GameController(context).EditGamesPost(context));
			routeConfig.AddGet("/gameDatails/{(?<id>[0-9]+)}", context => new GameController(context).GameDetailsGet(context));

			routeConfig.AddGet("/addToCart/{(?<id>[0-9]+)}", context => new ShoppingController(context).AddToCartGet(context));
			routeConfig.AddGet("/removeFromCart/{(?<id>[0-9]+)}", context => new ShoppingController(context).RemoveFromCartGet(context));
			routeConfig.AddGet("/orderSuccess", context => new ShoppingController(context).OrderSucces());
			routeConfig.AddGet("/checkout", context => new ShoppingController(context).CheckoutGet(context));
			routeConfig.AddPost("/checkout", context => new ShoppingController(context).CheckoutPost(context));
		}
	}
}
