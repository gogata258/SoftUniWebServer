namespace TestApplication.Controllers
{
	using Model;
	using Server.Enums;
	using Server.HTTP.Contracts;
	using Server.HTTP.Response;
	using Views;

	public class UserController
	{
		public IHttpResponse RegisterGet(IHttpRequest request)
		{
			return new ViewResponse(HttpStatusCode.OK, new RegisterView("/register", request));
		}
		public IHttpResponse RegisterPost(string name)
		{
			return new RedirectResponse($"/user/{name}");
		}
		public IHttpResponse Details(string name, IHttpRequest request)
		{
			Model model = new Model { ["name"] = name };
			return new ViewResponse(HttpStatusCode.OK, new UserDetailsView(model, request));
		}
	}
}
