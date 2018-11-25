using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore_App.Services.Contracts
{
	using ViewModel.Game;
	public interface IGameService
	{
		Task<bool> Add(DetailsGameViewModel game);
		ICollection<GameViewItemViewModel> GetAllGames();
		ICollection<CardGameViewModel> GetActiveGames();
		Task<DetailsGameViewModel> Get(int id);
		void UpdateGameInfo(DetailsGameViewModel game, int id);
		Task DeleteGame(int id);
		ICollection<CardGameViewModel> GetOwnedGames(int id);
	}
}
