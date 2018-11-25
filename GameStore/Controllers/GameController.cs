using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameStore_App.Controllers
{
	using Server.HTTP.Contracts;
	using Services;
	using Services.Contracts;
	using ViewModel.Game;

	public class GameController : BaseController
	{
		private readonly string AddGameView = @"/Game/GameAdd";
		private readonly string AllGamesView = @"/Game/AllGames";
		private readonly string EditGameView = @"/Game/GameEdit";
		private readonly string DeleteGameView = @"/Game/GameDelete";
		private readonly string GameDetailsView = @"/Game/GameDetails";

		readonly IGameService service;
		public GameController(IHttpRequest request) : base(request)
		{
			service = new GameService();
		}
		internal IHttpResponse AddGamesGet()
		{
			return FileViewResponse(AddGameView);
		}
		internal IHttpResponse AddGamesPost(IHttpRequest request)
		{
			if (!CheckAdmin(request))
				return RedirectResponse("/");
			if (!ValidateGameData(request)) FileViewResponse(AddGameView);
			DetailsGameViewModel game = GetGameViewData(request);

			if (service.Add(game).Result == false) AddGamesGet();
			return RedirectResponse("/");
		}
		internal IHttpResponse ViewAllGamesGet(IHttpRequest context)
		{
			string table = string.Empty;
			ICollection<GameViewItemViewModel> games = service.GetAllGames();
			foreach (var game in games)
				table += GameViewTableEntryHtml(game);
			ViewData["table"] = table;
			return FileViewResponse(AllGamesView);
		}
		internal IHttpResponse EditGamesGet(IHttpRequest context)
		{
			int id = int.Parse(context.UrlParameters["id"]);
			DetailsGameViewModel game = service.Get(id).Result;
			SetGameViewData(game);
			return FileViewResponse(EditGameView);
		}

		internal IHttpResponse EditGamesPost(IHttpRequest context)
		{
			int id = int.Parse(context.UrlParameters["id"]);
			DetailsGameViewModel game = GetGameViewData(context);
			service.UpdateGameInfo(game, id);
			return RedirectResponse("/allGames");
		}

		internal IHttpResponse DeleteGamePost(IHttpRequest context)
		{
			int id = int.Parse(context.UrlParameters["id"]);
			service.DeleteGame(id);
			return RedirectResponse("/allGames");
		}

		internal IHttpResponse GameDetailsGet(IHttpRequest context)
		{
			int id = int.Parse(context.UrlParameters["id"]);
			DetailsGameViewModel game = service.Get(id).Result;
			SetGameViewData(game);
			return FileViewResponse(GameDetailsView);
		}

		internal IHttpResponse DeleteGameGet(IHttpRequest context)
		{
			int id = int.Parse(context.UrlParameters["id"]);
			DetailsGameViewModel game = service.Get(id).Result;
			SetGameViewData(game);
			return FileViewResponse(DeleteGameView);
		}
		#region Helpers
		private void SetGameViewData(DetailsGameViewModel game)
		{
			ViewData["title"] = game.Title;
			ViewData["description"] = game.Description;
			ViewData["cover"] = game.Cover;
			ViewData["price"] = $"{game.Price:f2}";
			ViewData["size"] = $"{game.Size:f1}";
			ViewData["trailer"] = game.Trailer;
			ViewData["releaseDate"] = game.ReleaseDate.ToString("yyyy-MM-dd");
			ViewData["trailerId"] = game.TrailerId;
		}
		private DetailsGameViewModel GetGameViewData(IHttpRequest request)
		{
			return new DetailsGameViewModel(
				request.FormData["title"],
				decimal.Parse(request.FormData["price"]),
				float.Parse(request.FormData["size"]),
				request.FormData["trailer"],
				request.FormData["cover"],
				request.FormData["description"],
				request.FormData["releaseDate"]
				);
		}
		private string GameViewTableEntryHtml(GameViewItemViewModel game)
		{
			return "" +
				"<tr class='table-warning'>" +
					$"<th scope='row'>{game.Id}</th>" +
					$"<td>{game.Title}</td>" +
					$"<td>{game.Size:f1} GB</td>" +
					$"<td>{game.Price:f2} &euro;</td>" +
					"<td>" +
						$"<a href='/gameDatails/{game.Id}' class='btn btn-success btn-sm'>Details</a>" +
						$"<a href='/editGame/{game.Id}' class='btn btn-warning btn-sm'>Edit</a>" +
						$"<a href='/deleteGame/{game.Id}' class='btn btn-danger btn-sm'>Delete</a>" +
					"</td>";
		}
		#region Validation
		private bool ValidateGameData(IHttpRequest request)
		{
			bool isValid = true;
			const string httpStr = "http://";
			const string httpsStr = "https://";

			if (!ValidateValue(request.FormData["title"], @"[A-Z].{3,100}")) isValid = false;
			if (request.FormData["description"].Length < 20) isValid = false;
			if (!ValidateValue(request.FormData["size"], @"[0-9]{2}.[0-9]")) isValid = false;
			if (request.FormData["cover"] is string cover)
				if(!Uri.IsWellFormedUriString(cover, UriKind.RelativeOrAbsolute) && 
					cover.Substring(0, httpsStr.Length) != httpsStr && 
					cover.Substring(0, httpStr.Length) != httpStr)
					isValid = false;
			if (!ValidateValue(request.FormData["price"], "[0-9]{2}.[0-9]{2}")) isValid = false;
			if (!ValidateValue(request.FormData["trailer"], @"(?<=https:\/\/www\.youtube\.com\/watch\?v=)[a-zA-Z0-9-]{11}")) isValid = false;

			return isValid;
		}
		private bool ValidateValue(string value, string patter)
		{
			Regex regex = new Regex(patter);
			return regex.IsMatch(value);
		}
		#endregion
		#endregion
	}
}
