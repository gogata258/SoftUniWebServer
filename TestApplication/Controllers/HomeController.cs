namespace TestApplication.Controllers
{
	using Server.Enums;
	using Server.HTTP.Contracts;
	using Server.HTTP.Response;
	using Views;

	public class HomeController
	{
		public IHttpResponse Index(IHttpRequest request)
		{
			return new ViewResponse(HttpStatusCode.OK, new HomeIndexView("/Index", request));
		}
	}
}
