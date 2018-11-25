using GameStore_DAL;
using GameStore_Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore_App.Services
{
	using Contracts;
	using Microsoft.EntityFrameworkCore;
	using ViewModel.Game;
	public class GameService : IGameService
	{
		public async Task<bool> Add(DetailsGameViewModel game)
		{
			using (Context db = new Context())
			{
				if (Exists(game)) return false;
				await db.Games.AddAsync(new Game(game.Title, game.Trailer, game.Cover, game.Size, game.Price, game.Description, game.ReleaseDate));
				await db.SaveChangesAsync();
			}
			return true;
		}

		public async Task DeleteGame(int id)
		{
			using (Context db = new Context())
			{
				if (db.Games.Any(x => x.Id == id))
				{
					db.Games.Find(id).IsDeleted = true;
					await db.SaveChangesAsync();
				}
				else throw new System.ArgumentOutOfRangeException($"{id} id out of range");
			}
		}

		public async Task<DetailsGameViewModel> Get(int id)
		{
			using (Context db = new Context())
			{
				if (db.Games.Any(x => x.Id == id && x.IsDeleted == false))
				{
					Game foundGame = await db.Games.FindAsync(id);
					return new DetailsGameViewModel(foundGame.Title, foundGame.Price, foundGame.Size, foundGame.Trailer, foundGame.Cover, foundGame.Description, foundGame.ReleaseDate);
				}
				else throw new System.ArgumentOutOfRangeException($"{id} id out of range");
			}
		}

		public ICollection<GameViewItemViewModel> GetAllGames()
		{
			ICollection<GameViewItemViewModel> gameList = new List<GameViewItemViewModel>();
			using (Context db = new Context())
				db.Games.ToList().ForEach(x => gameList.Add(new GameViewItemViewModel(x.Id, x.Title, x.Size, x.Price)));
			return gameList;
		}
		public ICollection<CardGameViewModel> GetActiveGames()
		{
			ICollection<CardGameViewModel> gameList = new List<CardGameViewModel>();
			using (Context db = new Context())
				db.Games.Where(x => x.IsDeleted == false).ToList().ForEach(x => gameList.Add(new CardGameViewModel(x.Id, x.Title, x.Price, x.Size, x.Cover, x.Description, x.ReleaseDate)));
			return gameList;
		}

		public void UpdateGameInfo(DetailsGameViewModel game, int id)
		{
			using (Context db = new Context())
			{
				if (!Exists(id)) throw new System.ArgumentOutOfRangeException($"No such id in database: {id}");
				Game foundGame = db.Games.Find(id);

				foundGame.Cover = game.Cover;
				foundGame.Description = game.Description;
				foundGame.Price = game.Price;
				foundGame.ReleaseDate = game.ReleaseDate;
				foundGame.Size = game.Size;
				foundGame.Title = game.Title;
				foundGame.Trailer = game.Trailer;

				db.Games.Update(foundGame);
				db.SaveChanges();
			}
		}

		bool Exists(DetailsGameViewModel game) => Exists(game.Title);
		bool Exists(string title)
		{
			using (Context db = new Context())
				return db.Games.Any(x => x.Title == title);
		}
		bool Exists(int id)
		{
			using (Context db = new Context())
				return db.Games.Any(x => x.Id == id);
		}

		public ICollection<CardGameViewModel> GetOwnedGames(int id)
		{
			using (Context db = new Context())
			{
				return db.Users.Include(u => u.Games).ToList().Find(x => x.Id == id).Games.Select(x =>
				{
					var game = db.Games.Find(x.GameId);
					return new CardGameViewModel(game.Id, game.Title, game.Price, game.Size, game.Cover, game.Description, game.ReleaseDate);
				}).ToList();
			}
		}
	}
}
