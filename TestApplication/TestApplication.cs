namespace TestApplication
{
	using Controllers;
	using Server.Abstractions;
	using Server.Routing.Contracts;

	public class TestApplication : ApplicationBase
	{

		public override void Configure(IAppRouteConfig routeConfig)
		{
			SetAnonimousPaths(routeConfig);
			SetRouting(routeConfig);
		}

		protected override void SetAnonimousPaths(IAppRouteConfig routeConfig)
		{
			routeConfig.AnonymousPaths.Add("/");
			routeConfig.AnonymousPaths.Add("/register");
		}

		protected override void SetRouting(IAppRouteConfig routeConfig)
		{
			routeConfig.AddGet("/", context => new HomeController().Index(context));

			routeConfig.AddGet("/register", context => new UserController().RegisterGet(context));
			routeConfig.AddPost("/register", context => new UserController().RegisterPost(context.FormData["name"]));

			routeConfig.AddGet("/user/{(?<name>[a-zA-Z]+)}", context => new UserController().Details(context.UrlParameters["name"], context));
		}
	}
}
