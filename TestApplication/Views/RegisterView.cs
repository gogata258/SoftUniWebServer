namespace TestApplication.Views
{
	using Server.HTTP.Contracts;
	internal class RegisterView : ViewBase
	{
		public RegisterView(string path, IHttpRequest request) : base(path, request)
		{

		}
		public override string View()
		{
			return
				"<body>" +
				"	<form method=\'POST\'>" +
				"		Name</br>" +
				"		<input type=\'text\' name=\'name\' /><br/>" +
				"		<input type=\'submit\' />" +
				"	</form>" +
				"</body>";
		}
	}
}