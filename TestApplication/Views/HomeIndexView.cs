namespace TestApplication.Views
{
	using Server.HTTP.Contracts;

	internal class HomeIndexView : ViewBase
	{
		public HomeIndexView(string path, IHttpRequest request) : base(path, request)
		{

		}
		public override string View()
		{
			return "<body><h1>Hello Server</h1></body>";
		}
	}
}