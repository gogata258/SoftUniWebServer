namespace Server.Contracts
{
	using Server.Routing.Contracts;
	public interface IApplication
	{
		void Configure(IAppRouteConfig routeConfig);
	}
}
