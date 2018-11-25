using GameStore_DAL;
using GameStore_Model;
using System.Threading.Tasks;

namespace GameStore_App.Services
{
	using Contracts;
	using GameStore_App.ViewModel.Game;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Linq;
	using ViewModel;

	public class ShoppingService : IShoppingService
	{
		public async Task Purchase(Cart cart, int userId)
		{
			List<int> gameIds = new List<int>();
			foreach (var productId in cart.ProductIds)
				if (!CheckOwned(productId, userId))
					gameIds.Add(productId);
			using (Context db = new Context())
			{
				User foundUser = db.Users.Find(userId);
				foreach (var gameId in gameIds)
					foundUser.Games.Add(new UserGame(gameId));
				await db.SaveChangesAsync();
			}
		}

		public bool CheckOwned(int gameId, int userID)
		{
			using (Context db = new Context())
				return db.Users.Include(u => u.Games).ToList().Find(x => x.Id == userID).Games.Any(x => x.GameId == gameId);
		}

		public CartViewGameItemViewModel Get(int id)
		{
			using (Context db = new Context())
			{
				var foundItem = db.Games.Find(id);
				return new CartViewGameItemViewModel(foundItem.Id, foundItem.Cover, foundItem.Description, foundItem.Title, foundItem.Price);
			}
		}

		public decimal GetPrice(int id)
		{
			using (Context db = new Context())
				return db.Games.Find(id).Price;
		}
	}
}
