﻿namespace ByTheCake_Model
{
	public class ProductOrder
	{
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; }
		public ProductOrder(int productId)
		{
			ProductId = productId;
		}
	}
}
