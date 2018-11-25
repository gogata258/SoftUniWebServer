using System.Text;

namespace Server.HTTP.Response
{
	using HTTP.Contracts;
	using Server.Enums;
	public abstract class HttpResponse : IHttpResponse
	{
		protected HttpResponse()
		{
			Headers = new HttpHeaderCollection();
			Cookies = new HttpCookieCollection();
		}
		public void AddHeader(string title, string redirectUrl)
		{
			Headers.Add(new HttpHeader(title, redirectUrl));
		}
		#region Member Properties 
		public IHttpHeaderCollection Headers { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public string StatusMessage => StatusCode.ToString();
		public IHttpCookieCollection Cookies { get; set; }

		public override string ToString()
		{
			StringBuilder response = new StringBuilder();
			response.AppendLine($"HTTP/1.1 {(int)StatusCode} {StatusMessage}");
			response.AppendLine(Headers.ToString());
			return response.ToString();
		}
		#endregion
	}
}
