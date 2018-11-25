using System;
using System.Collections.Generic;

namespace Server.Routing
{
	using Contracts;
	using Server.Handlers.Contracts;
	using Server.Handlers;

	public class RoutingContext : IRoutingContext
	{
		public RoutingContext(RequestHandler handler, List<string> parameters)
		{
			Parameters = parameters;
			RequestHandler = handler;
		}
		#region Member Properties
		public IEnumerable<string> Parameters { get; }
		public IRequestHandler RequestHandler { get; }
		#endregion
	}
}
