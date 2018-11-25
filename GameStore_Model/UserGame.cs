namespace GameStore_Model
{
	public class UserGame
	{
		public int UserId { get; set; }
		public User User { get; set; }
		public int GameId { get; set; }
		public Game Game { get; set; }
		public UserGame(int gameId)
		{
			GameId = gameId;
		}
	}
}
