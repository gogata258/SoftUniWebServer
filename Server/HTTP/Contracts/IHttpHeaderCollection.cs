using System.Collections.Generic;

namespace Server.HTTP.Contracts
{
	public interface IHttpHeaderCollection
	{ 
		void Add(HttpHeader header);
		void Add(string key, string value);
		bool ContainsKey(string key);
		ICollection<HttpHeader> GetHeader(string key);
	}
}
