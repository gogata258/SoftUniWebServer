using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore_Model
{
	public class Game
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		//NOTE: keep only the youtube ID
		[Required]
		public string Trailer { get; set; }
		public string Cover { get; set; }
		//NOTE: in GB
		public double Size { get; set; }
		public decimal Price { get; set; }
		public bool IsDeleted { get; set; }
		[Required]
		public string Description { get; set; }
		public DateTime ReleaseDate { get; set; }
		public List<UserGame> Users { get; set; }

		public Game(string title, string trailer, string cover, double size, decimal price, string description, DateTime releaseDate)
		{
			Title = title ?? throw new ArgumentNullException(nameof(title));
			Trailer = trailer ?? throw new ArgumentNullException(nameof(trailer));
			Cover = cover ?? throw new ArgumentNullException(nameof(cover));
			Size = size;
			Price = price;
			Description = description ?? throw new ArgumentNullException(nameof(description));
			ReleaseDate = releaseDate;
			IsDeleted = false;
		}
	}
}
