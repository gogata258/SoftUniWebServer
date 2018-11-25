namespace Server.HTTP.Contracts
{
	using Server.Enums;
	public interface IHttpResponse
	{
		IHttpHeaderCollection Headers { get; set; }
		IHttpCookieCollection Cookies { get; set; }
		HttpStatusCode StatusCode { get; set; }
	}
}
