using System;

namespace GameStore_App.ViewModel.Game
{
	public class CartViewGameItemViewModel
	{
		public int Id { get; set; }
		public string Cover { get; set; }
		string Description { get; set; }
		public string DescriptionShort
		{
			get
			{
				if (Description.Length > 100) return Description.Substring(0, 100);
				else return Description;
			}
		}
		public string Title { get; set; }
		public decimal Price { get; set; }
		public CartViewGameItemViewModel(int id, string cover, string description, string title, decimal price)
		{
			Id = id;
			Cover = cover ?? throw new ArgumentNullException(nameof(cover));
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Title = title ?? throw new ArgumentNullException(nameof(title));
			Price = price;
		}
	}
}
