using System;

namespace GameStore_App.ViewModel.Game
{
	public class DetailsGameViewModel
	{ 
		public string Title { get; set; }
		public decimal Price { get; set; }
		public double Size { get; set; }
		public string TrailerId { get; private set; }
		public string Trailer
		{
			get => $"https://www.youtube.com/watch?v={TrailerId}";
			set
			{
				if (value.Length == 11) TrailerId = value;
				else TrailerId = value.Substring(value.Length - 11, 11);
			}
		}
		public string Cover { get; set; }
		public string Description { get; set; }
		public string DescriptionShort {
			get
			{
				if (Description.Length <= 300) return Description;
				else return Description.Substring(0, 300);
			}
		}
		public DateTime ReleaseDate { get; set; }

		public DetailsGameViewModel(string title, decimal price, double size, string trailer, string cover, string description, string releaseDate) : this(title, price, size, cover, description, trailer)
		{
			ReleaseDate = DateTime.ParseExact(releaseDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
		}
		private DetailsGameViewModel(string title, decimal price, double size, string cover, string description, string trailer)
		{
			Title = title ?? throw new ArgumentNullException(nameof(title));
			Price = price;
			Size = size;
			Cover = cover;
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Trailer = trailer ?? throw new ArgumentNullException(nameof(trailer));
		}
		public DetailsGameViewModel(string title, decimal price, double size, string trailer, string cover, string description, DateTime releaseDate) : this(title, price, size, cover, description, trailer)
		{
			ReleaseDate = releaseDate;
		}
	}
}
