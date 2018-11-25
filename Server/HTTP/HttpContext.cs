namespace Server.HTTP
{
	using Contracts;
	public class HttpContext : IHttpContext
	{
		private readonly IHttpRequest request;
		public HttpContext(string requestString)
		{
			request = new HttpRequest(requestString);
		}
		public IHttpRequest Request { get => request; }
	}
}
