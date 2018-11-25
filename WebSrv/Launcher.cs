namespace WebSrv
{
	using Server.Contracts;
	using Server.Routing;
	using Server.Routing.Contracts;

	public class Launcher : IRunnable
	{
		Server.WebServer webServer;
		static void Main()
		{
			new Launcher().Run();
		}
		public void Run()
		{
			RunGameStore();
		}
		void RunCakeApp()
		{
			IApplication app = new ByTheCake_App.ByTheCake();
			IAppRouteConfig routeConfig = new AppRouteConfig(app);
			app.Configure(routeConfig);

			webServer = new Server.WebServer(8000, routeConfig);
			webServer.Run();
		}
		void RunGameStore()
		{
			IApplication app = new GameStore_App.GameStoreApp();
			IAppRouteConfig routeConfig = new AppRouteConfig(app);
			app.Configure(routeConfig);

			webServer = new Server.WebServer(8000, routeConfig);
			webServer.Run();
		}
	}
}
