using System;

namespace GameStore_App.ViewModel.Game
{
	public class GameViewItemViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double Size { get; set; }
		public decimal Price { get; set; }
		public GameViewItemViewModel(int id, string title, double size, decimal price)
		{
			Id = id;
			Title = title ?? throw new ArgumentNullException(nameof(title));
			Size = size;
			Price = price;
		}
	}
}
