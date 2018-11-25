namespace TestApplication.Views
{
	using Server.HTTP.Contracts;

	internal class NotFoundView : ViewBase
	{
		public NotFoundView(string path, IHttpRequest request) : base(path, request)
		{

		}
		public override string View()
		{
			return "<body><h3>View not found !!!</h3></body>";
		}
	}
}