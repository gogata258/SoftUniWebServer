using System.IO;

namespace TestApplication.Views
{
	using Server.Contracts;
	using Server.HTTP.Contracts;

	public abstract class ViewBase : IView
	{
		public IHttpRequest HttpRequest { get; }
		public string PATH_VIEW { get; }
		public string PATH_ENVIRONMENT => "Applications/ByTheCake/Resources";
		public ViewBase(string path, IHttpRequest request)
		{
			PATH_VIEW = PATH_ENVIRONMENT + path;
			HttpRequest = request;
		}
		public virtual string View()
		{
			return File.ReadAllText(PATH_VIEW);
		}
	}
}
