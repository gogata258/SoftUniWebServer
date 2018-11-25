using System;
using System.Collections.Generic;
using System.Text;

namespace Server.HTTP
{
	using Contracts;

	public class HttpHeaderCollection : IHttpHeaderCollection
	{
		const string INVALID_KEY_EXCEPTION_MESSAGE = "The given key: '{0}' is not in header collection";
		private readonly IDictionary<string, ICollection<HttpHeader>> headers;
		public HttpHeaderCollection()
		{
			headers = new Dictionary<string, ICollection<HttpHeader>>();
		}

		public void Add(HttpHeader header)
		{
			CustomValidator.ThrowIfNull(header, nameof(header));
			if (!ContainsKey(header.Key)) headers[header.Key] = new List<HttpHeader>();
			headers[header.Key].Add(header);
		}
		public void Add(string key, string value)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			CustomValidator.ThrowIfNullOrEmpty(value, nameof(value));
			Add(new HttpHeader(key, value));
		}
		public bool ContainsKey(string key)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			return headers.ContainsKey(key);
		}

		public ICollection<HttpHeader> GetHeader(string key)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			return (ContainsKey(key)) ? headers[key] : throw new InvalidOperationException(string.Format(INVALID_KEY_EXCEPTION_MESSAGE, key));
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var header in headers)
				foreach(var value in header.Value)
					sb.AppendLine($"{header.Key}: {value.Value}");
			return sb.ToString();
		}
	}
}
