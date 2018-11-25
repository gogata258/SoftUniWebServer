using System;
using System.Collections.Generic;

namespace Server.Routing.Contracts
{
	using Enums;
	using Handlers;
	using HTTP.Contracts;
	public interface IAppRouteConfig
	{ 
		IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes { get; }
		ICollection<string> AnonymousPaths { get; }
		void AddGet(string route, Func<IHttpRequest, IHttpResponse> handler);
		void AddPost(string route, Func<IHttpRequest, IHttpResponse> handler);
		void AddRoute(string route, HttpRequestMethod method, RequestHandler httpHandler);
	}
}
