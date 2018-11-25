using System;

namespace Server.Abstractions
{
	using Contracts;
	using Routing.Contracts;

	public abstract class ApplicationBase : IApplication
	{
		public virtual void Configure(IAppRouteConfig routeConfig)
		{
			InitilizeDatabase();
			SetAnonimousPaths(routeConfig);
			SetRouting(routeConfig);
		}
		protected virtual void InitilizeDatabase()
		{
		
		}
		protected abstract void SetAnonimousPaths(IAppRouteConfig routeConfig);
		protected abstract void SetRouting(IAppRouteConfig routeConfig);
	}
}
