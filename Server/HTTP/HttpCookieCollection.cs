using System;
using System.Collections.Generic;
using System.Collections;

namespace Server.HTTP
{
	using Contracts;

	public class HttpCookieCollection : IHttpCookieCollection
	{
		private readonly IDictionary<string, HttpCookie> cookies;
		public HttpCookieCollection()
		{
			cookies = new Dictionary<string, HttpCookie>();
		}
		public void Add(HttpCookie cookie)
		{
			CustomValidator.ThrowIfNull(cookie, nameof(cookie));
			cookies[cookie.Key] = cookie;
		}
		public void Add(string key, string value)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			CustomValidator.ThrowIfNullOrEmpty(value, nameof(value));
			cookies[key] = new HttpCookie(key, value);
		}
		public bool ContainsKey(string key)
		{
			CustomValidator.ThrowIfNull(key, nameof(key));
			return cookies.ContainsKey(key);
		}
		public HttpCookie Get(string key)
		{
			CustomValidator.ThrowIfNull(key, nameof(key));
			return (ContainsKey(key)) ? cookies[key]: throw new InvalidOperationException($"The given key {key} is not in the cookie collection");
		}
		public IEnumerator GetEnumerator() => cookies.Values.GetEnumerator();
		IEnumerator<HttpCookie> IEnumerable<HttpCookie>.GetEnumerator() => cookies.Values.GetEnumerator();
	}
}
