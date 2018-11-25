using ByTheCake_DAL;
using ByTheCake_Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ByTheCake_App.Services
{
	using Contracts;
	using ViewModel;
	using ViewModel.Account;
	using ViewModel.Product;

	public class ShoppingService : IShoppingService
	{
		public ICollection<OrderViewModel> GetOrders(int userId)
		{
			List<OrderViewModel> orderList = new List<OrderViewModel>();
			using (Context dc = new Context())
				dc.Orders.Where(x => x.UserId == userId).ToList().ForEach(o => orderList.Add(new OrderViewModel(
					o.Id,
					o.DateOfCreation,
					dc.ProductOrders.Where(po => po.OrderId == o.Id).ToList().Sum(po => dc.Products.Find(po.ProductId).Price)
					)));
			return orderList;
		}
		public ICollection<ProductSearchItemViewModel> GetProductInOrder(int orderId, out DateTime creationDate)
		{
			List<ProductSearchItemViewModel> products = new List<ProductSearchItemViewModel>();
			using (Context dc = new Context())
			{
				foreach (var prodcut in dc.ProductOrders.Where(po => po.OrderId == orderId).ToList().Select(po => dc.Products.Find(po.ProductId)).ToList())
					products.Add(new ProductSearchItemViewModel(prodcut.Name, prodcut.Price, prodcut.Id));
				creationDate = dc.Orders.Find(orderId).DateOfCreation;
			}
			return products;
		}
		public async void Order(Cart cart, int userId)
		{
			List<ProductOrder> tempList = new List<ProductOrder>();
			using (Context db = new Context())
			{
				foreach (var item in cart.ProductIds)
					tempList.Add(new ProductOrder(item));
				Order order = new Order(userId, tempList);
				await db.Orders.AddAsync(order);
				await db.SaveChangesAsync();
			}
		}

		public List<ProductViewModel> GetProductsInCart(Cart cart)
		{
			IProductService service = new ProductService();
			List<ProductViewModel> products = new List<ProductViewModel>();
			foreach (var id in cart.ProductIds)
				products.Add(service.Find(id));
			return products;
		}
	}
}
