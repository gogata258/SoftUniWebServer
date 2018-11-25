namespace TestApplication.Views
{
	using Model;
	using Server.HTTP.Contracts;

	internal class UserDetailsView : ViewBase
	{
		private readonly Model model;
		public UserDetailsView(Model model, IHttpRequest request) : base("/", request)
		{
			this.model = model;
		}
		public override string View()
		{
			return $"<body><h1>Hello, {model["name"]}!</h1></body>";
		}
	}
}