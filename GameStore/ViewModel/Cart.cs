namespace GameStore_App.ViewModel
{
	using System.Collections.Generic;
	public class Cart
	{
		public const string CartSessionKey = "CurrentShoppingCart";
		//TODO change this when ready
		public IList<int> ProductIds{ get; private set; }
		public Cart()
		{
			ProductIds = new List<int>();
		}
		public void Add(int productId)
		{
			if (!ProductIds.Contains(productId)) ProductIds.Add(productId);
		}
	}
}
