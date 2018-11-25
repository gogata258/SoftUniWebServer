using System.Collections.Generic;

namespace Server.HTTP.Contracts
{
	using Server.Enums;
	public interface IHttpRequest
	{
		IDictionary<string, string> FormData { get; }
		IDictionary<string, string> QueryParameters { get; }
		IDictionary<string, string> UrlParameters { get; }
		IHttpCookieCollection Cookies { get; }
		IHttpHeaderCollection Headers { get; }
		IHttpSession Session { get; set; }
		HttpRequestMethod Method { get; }
		string Url { get; }
		string Path { get; }
		void AddUrlParameters(string key, string value);
	}
}
