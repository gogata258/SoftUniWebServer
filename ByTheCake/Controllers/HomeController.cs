namespace ByTheCake_App.Controllers
{
	using Server.Abstractions;
	using Server.HTTP.Contracts;
	public class HomeController : ControllerBase
	{
		private const string IndexView = @"Home\Index";
		private const string AboutView = @"Home\About";
		protected override string ApplicationDirectory => System.Environment.CurrentDirectory;
		public IHttpResponse Index() => FileViewResponse(IndexView);
		public IHttpResponse About() => FileViewResponse(AboutView);

	}
}
