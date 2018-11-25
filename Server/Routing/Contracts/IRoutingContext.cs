using System.Collections.Generic;

namespace Server.Routing.Contracts
{ 
	using Server.Handlers.Contracts;
	public interface IRoutingContext
	{
		IEnumerable<string> Parameters { get; }
		IRequestHandler RequestHandler { get; }
	}
}
