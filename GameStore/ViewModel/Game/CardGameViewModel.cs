using System;

namespace GameStore_App.ViewModel.Game
{
	public class CardGameViewModel
	{
		public string Title { get; set; }
		public decimal Price { get; set; }
		public double Size { get; set; }
		public string Cover { get; set; }
		string Description { get; set; }
		public string DescriptionShort
		{
			get
			{
				if (Description.Length <= 300) return Description;
				else return Description.Substring(0, 300);
			}
		}
		public DateTime ReleaseDate { get; set; }
		public int Id { get; set; }
		public CardGameViewModel(int id, string title, decimal price, double size, string cover, string description, DateTime releaseDate)
		{
			Id = id;
			Title = title ?? throw new ArgumentNullException(nameof(title));
			Price = price;
			Size = size;
			Cover = cover;
			Description = description ?? throw new ArgumentNullException(nameof(description));
			ReleaseDate = releaseDate;
		}
	}
}
