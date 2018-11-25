using System;

namespace Server.Handlers
{
	using Contracts;
	using HTTP.Contracts;
	using Server.HTTP;

	public class RequestHandler : IRequestHandler
	{
		private readonly Func<IHttpRequest, IHttpResponse> function;
		public RequestHandler(Func<IHttpRequest, IHttpResponse> func)
		{
			CustomValidator.ThrowIfNull(func, nameof(func));
			function = func;
		}

		public IHttpResponse Handle(IHttpContext context)
		{
			string sessionIdToSend = null;

			if (!context.Request.Cookies.ContainsKey(SessionStore.SessionCookieKey))
			{
				var tempSessionId = Guid.NewGuid().ToString();
				context.Request.Session = SessionStore.Get(tempSessionId);
				sessionIdToSend = tempSessionId;
			}
			IHttpResponse httpResponse = function(context.Request);
			if(sessionIdToSend != null) httpResponse.Headers.Add(HttpHeader.SetCookie, $"{SessionStore.SessionCookieKey}={sessionIdToSend}; HttpOnly; path=/");
			if (!httpResponse.Headers.ContainsKey(HttpHeader.ContentType)) httpResponse.Headers.Add(HttpHeader.ContentType, "text/plain");
			foreach (var cookie in httpResponse.Cookies) httpResponse.Headers.Add(HttpHeader.SetCookie, cookie.ToString());
			return httpResponse;
		}
	}
}
