using System;

namespace ByTheCake_App.ViewModel.Account
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public DateTime CreateOn { get; set; }
		public decimal Total { get; set; }
		public OrderViewModel(int id, DateTime createOn, decimal total)
		{
			Id = id;
			CreateOn = createOn;
			Total = total;
		}
	}
}
