using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Routing
{
	using Contracts;
	using Common;
	using Server.Contracts;
	using Server.Enums;
	using Server.Handlers;
	using Server.HTTP.Contracts;

	public class AppRouteConfig : IAppRouteConfig
	{
		private readonly Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> routes;
		public IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes => routes;
		public ICollection<string> AnonymousPaths { get; }

		public AppRouteConfig(IApplication app)
		{
			AnonymousPaths = new List<string>();
			routes = new Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>>();

			foreach (HttpRequestMethod requestMethod in Enum.GetValues(typeof(HttpRequestMethod)))
				routes.Add(requestMethod, new Dictionary<string, RequestHandler>());
		}
		public void AddRoute(string route, HttpRequestMethod method, RequestHandler httpHandler)
		{
			routes[method].Add(route, httpHandler);
		}
		public void AddGet(string route, Func<IHttpRequest, IHttpResponse> handler)
		{
			AddRoute(route, HttpRequestMethod.GET, new RequestHandler(handler));
		}
		public void AddPost(string route, Func<IHttpRequest, IHttpResponse> handler)
		{
			AddRoute(route, HttpRequestMethod.POST, new RequestHandler(handler));
		}
	}
}
