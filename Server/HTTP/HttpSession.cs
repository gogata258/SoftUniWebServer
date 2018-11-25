using System.Collections.Generic;

namespace Server.HTTP
{
	using Contracts;
	using System;

	public class HttpSession : IHttpSession
	{
		private readonly IDictionary<string, object> values;
		public HttpSession(string id)
		{
			CustomValidator.ThrowIfNullOrEmpty(id, nameof(id));
			Id = id;
			values = new Dictionary<string, object>();
		}
		public string Id { get; private set; }
		public void Add(string key, object value)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			CustomValidator.ThrowIfNull(value, nameof(value));
			values[key] = value;
		}
		public bool Contains(string key)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			return values.ContainsKey(key);
		}
		public void Clear() => values.Clear();
		public object Get(string key)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			return (Contains(key)) ? values[key] : throw new InvalidOperationException("The given key does not exist in Session values collection");
		}
		public T Get<T>(string key) => (T)Get(key);
		public void Remove(string key)
		{
			CustomValidator.ThrowIfNullOrEmpty(key, nameof(key));
			if (Contains(key)) values.Remove(key);
		}
	}
}
