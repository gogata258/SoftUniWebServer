namespace GameStore_App.Controllers
{
	using Server.HTTP;
	using Server.HTTP.Contracts;
	using Services;
	using Services.Contracts;
	using ViewModel.Game;

	public class HomeController : BaseController
	{
		private readonly string HomeViewUser = @"/Home/Index";
		readonly IGameService service;

		public HomeController(IHttpRequest request) : base(request)
		{
			service = new GameService();
		}
		internal IHttpResponse Home(IHttpRequest context)
		{
			if(context.QueryParameters.ContainsKey("filter") && context.QueryParameters["filter"] == "Owned" && Authentication.IsAuthenticated == true)
			{
				int id = context.Session.Get<int>(SessionStore.SessionLoginId);
				string result = string.Empty;
				foreach (var game in service.GetOwnedGames(id))
					result = GetGameCardHtml(game);
				ViewData["cards"] = result;
				return FileViewResponse(HomeViewUser);
			}
			else
			{
				string result = string.Empty;
				foreach (var game in service.GetActiveGames())
					result = GetGameCardHtml(game);
				ViewData["cards"] = result;
				return FileViewResponse(HomeViewUser);
			}
		}
		private string GetGameCardHtml(CardGameViewModel game)
		{
			return "" +
				"<div class='card-group'>\n" +
					"<div class='card col-4 thumbnail'>\n" +
						$"<img class='card-image-top img-fluid img-thumbnail' onerror='https://i.ytimg.com/vi/BqJyluskTfM/maxresdefault.jpg' src='{game.Cover}'>\n" +
						$"<div class='card-body'>\n" +
							$"<h4 class='card-title'>{game.Title}</h4>\n" +
							$"<p class='card-text'><strong>Price</strong> - {game.Price:f2}&euro;</p>\n" +
							$"<p class='card-text'><strong>Size</strong> - {game.Size:f1} GB</p>\n" +
							$"<p class='card-text'>{game.DescriptionShort}</p>\n" +
						$"</div>\n" +
						$"<div class='card-footer'>\n" +
							$"<a class='card-button btn btn-outline-primary' name='info' href='/gameDetails/{game.Id}'>Info</a>\n" +
							$"<a class='card-button btn btn-primary' name='buy' href='/addToCart/{game.Id}'>Buy</a>\n" +
							$"<a style='display: {{{{{{adminDisplay}}}}}};' class='card-button btn btn-warning' name='edit' href='/gameEdit/{game.Id}'>Edit</a>\n" +
							$"<a style='display: {{{{{{adminDisplay}}}}}};' class='card-button btn btn-danger' name='delete' href='/gameDelete/{game.Id}'>Delete</a>\n" +
						$"</div>\n" +
					$"</div>\n" +
				$"</div>\n";
		}
	}
}
