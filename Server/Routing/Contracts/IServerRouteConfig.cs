using System.Collections.Generic;

namespace Server.Routing.Contracts
{
	using Server.Enums;
	public interface IServerRouteConfig
	{
		IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }
		ICollection<string> AnonymousPaths { get; }
	}
}
