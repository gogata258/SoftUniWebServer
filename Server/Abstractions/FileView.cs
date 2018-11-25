using Server.Contracts;

namespace Server.Abstractions
{
	public class FileView : IView
	{
		private readonly string htmlFile;
		public FileView(string htmlFileString)
		{
			htmlFile = htmlFileString;
		}
		public string View() => htmlFile;
	}
}