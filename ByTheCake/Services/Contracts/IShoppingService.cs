using System.Collections.Generic;
using System;

namespace ByTheCake_App.Services.Contracts
{
	using ViewModel.Product;
	using ViewModel;
	using ViewModel.Account;

	public interface IShoppingService
	{
		void Order(Cart cart, int userId);
		List<ProductViewModel> GetProductsInCart(Cart cart);
		ICollection<OrderViewModel> GetOrders(int userId);
		ICollection<ProductSearchItemViewModel> GetProductInOrder(int orderId, out DateTime creationDate);
	}
}
