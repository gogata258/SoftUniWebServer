using System.Threading.Tasks;

namespace GameStore_App.Services.Contracts
{
	using GameStore_App.ViewModel.Game;
	using ViewModel;

	public interface IShoppingService
	{
		Task Purchase(Cart cart, int id);
		bool CheckOwned(int gameId, int userID);
		CartViewGameItemViewModel Get(int id);
		decimal GetPrice(int id);
	}
}
