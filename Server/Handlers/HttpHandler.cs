using System.Text.RegularExpressions;
using System.Linq;

namespace Server.Handlers
{
	using Contracts;
	using HTTP.Contracts;
	using HTTP.Response;
	using Routing.Contracts;
	using Server.HTTP;

	public class HttpHandler : IRequestHandler
	{
		private readonly IServerRouteConfig routeConfing;
		public HttpHandler(IServerRouteConfig config)
		{
			CustomValidator.ThrowIfNull(config, nameof(config));
			routeConfing = config;
		}
		public IHttpResponse Handle(IHttpContext context)
		{
			try
			{
				bool isAllowed = false;
				foreach (var item in routeConfing.AnonymousPaths)
				{
					if (item == "/") continue;
					Regex rex = new Regex(item);
					if (rex.IsMatch(context.Request.Path)) isAllowed = true;
				}

				if (!routeConfing.AnonymousPaths.Contains(context.Request.Path) && isAllowed)
					if (context.Request.Session != null && !context.Request.Session.Contains(SessionStore.SessionLoginId))
						return new RedirectResponse(routeConfing.AnonymousPaths.First());
				foreach (var item in routeConfing.Routes[context.Request.Method])
				{
					Regex rex = new Regex(item.Key);
					Match match = rex.Match(context.Request.Path);
					if (!match.Success) continue;
					foreach (var param in item.Value.Parameters)
						context.Request.AddUrlParameters(param, match.Groups[param].Value);
					return item.Value.RequestHandler.Handle(context);
				}
			}
			catch (System.Exception e)
			{
				return new InternalServerErrorResonse(e);
			}
			return new NotFoundResponse();
		}
	}
}
