using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Server.HTTP
{
	using Contracts;
	using Server.Enums;
	using Server.Exceptions;

	public class HttpRequest : IHttpRequest
	{
		#region Method Properties
		public IDictionary<string, string> FormData { get; }
		public IDictionary<string, string> QueryParameters { get; }
		public IDictionary<string, string> UrlParameters { get; }
		public IHttpHeaderCollection Headers { get; }
		public HttpRequestMethod Method { get; private set; }
		public string Url { get; private set; }
		public string Path { get; private set; }
		public IHttpSession Session { get; set; }
		public IHttpCookieCollection Cookies { get; }
		#endregion
		public HttpRequest(string requestString)
		{
			CustomValidator.ThrowIfNullOrWhiteSpaced(requestString, nameof(requestString));

			Headers = new HttpHeaderCollection();
			UrlParameters = new Dictionary<string, string>();
			QueryParameters = new Dictionary<string, string>();
			FormData = new Dictionary<string, string>();
			Cookies = new HttpCookieCollection();

			ParseRequest(requestString);
		}

		private void ParseRequest(string requestString)
		{
			string[] requestLines = requestString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			string[] methodLineToken = requestLines.First().Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (methodLineToken.Length != 3 || methodLineToken[2].ToLower() != "http/1.1") BadRequestException.ThrowFromInvalidRequest();

			Method = ParseRequestMethod(methodLineToken.First().ToUpper());
			Url = methodLineToken[1];
			Path = Url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries).First();
			ParseHeaders(requestLines);
			ParseCookies();
			ParseParameters();
			if (Method == HttpRequestMethod.POST) ParseQuery(requestLines[requestLines.Length - 1], FormData);
			SetSession();
		}

		#region Helpers
		private void ParseCookies()
		{
			if (Headers.ContainsKey(HttpHeader.Cookie))
			{
				ICollection<HttpHeader> cookies = Headers.GetHeader(HttpHeader.Cookie);
				foreach (HttpHeader cookieHeader in cookies)
				{
					if (!cookieHeader.Value.Contains('=')) return;

					var splitCookie = cookieHeader.Value.Split(';', StringSplitOptions.RemoveEmptyEntries);
					if (!splitCookie.Any()) continue;

					foreach (var part in splitCookie)
					{
						string[] cookieKeyValuePair = part.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
						if (cookieKeyValuePair.Length != 2) continue;

						string key = cookieKeyValuePair[0].Trim();
						string value = cookieKeyValuePair[1].Trim();

						Cookies.Add(new HttpCookie(key, value, false));
					}
				}
			}
		}
		private void SetSession()
		{
			if (Cookies.ContainsKey(SessionStore.SessionCookieKey))
			{
				HttpCookie cookie = Cookies.Get(SessionStore.SessionCookieKey);
				Session = SessionStore.Get(cookie.Value);
			}
		}
		private void ParseParameters()
		{
			if (!Url.Contains('?')) return;
			string query = Url.Split('?')[1];
			ParseQuery(query, QueryParameters);
		}
		private void ParseQuery(string queryString, IDictionary<string, string> dict)
		{
			if (!queryString.Contains('=')) return;
			string[] queryPairs = queryString.Split('&');
			foreach (var item in queryPairs)
			{
				string[] pair = item.Split('=');
				if (pair.Length != 2) continue;
				dict.Add(pair.First(), System.Web.HttpUtility.UrlDecode(pair.Last()));
			}
		}
		private void ParseHeaders(string[] requestLines)
		{
			int endIndex = Array.IndexOf(requestLines, string.Empty);
			for (int i = 1; i < endIndex; i++)
			{
				string[] headerArgs = requestLines[i].Split(new[] { ": " }, StringSplitOptions.None);
				if (headerArgs.Length != 2) throw new BadRequestException($"Header is invalid {requestLines[i]}");
				Headers.Add(new HttpHeader(headerArgs.First(), headerArgs.Last().Trim()));
			}
			if (!Headers.ContainsKey("Host")) throw new BadRequestException("Host header missing from request");
		}
		private HttpRequestMethod ParseRequestMethod(string method)
		{
			if (Enum.TryParse(typeof(HttpRequestMethod), method, true, out object result))
				return (HttpRequestMethod)result;
			else throw new BadRequestException($"Invalid request type {method}");
		}
		public void AddUrlParameters(string key, string value)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			CustomValidator.ThrowIfNullOrEmpty(value, nameof(value));
			UrlParameters[key] = value;
		}
		#endregion
	}
}
