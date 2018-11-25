using Microsoft.EntityFrameworkCore;
using ByTheCake_Model;
using System;

namespace ByTheCake_DAL
{
	public class Context : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<ProductOrder> ProductOrders { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Product>()
				.HasMany(product => product.Orders)
				.WithOne(order => order.Product)
				.HasForeignKey(po => po.ProductId);
			builder.Entity<Order>()
				.HasMany(order => order.Products)
				.WithOne(product => product.Order)
				.HasForeignKey(po => po.OrderId);
			builder.Entity<ProductOrder>()
				.HasKey(po => new { po.OrderId, po.ProductId });
			builder.Entity<User>()
				.HasMany(u => u.Orders)
				.WithOne(o => o.User)
				.HasForeignKey(o => o.UserId);

			base.OnModelCreating(builder);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			const string conString = "Data Source=localhost;Database=ByTheCake;Integrated Security=True;";
			if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(conString);
			base.OnConfiguring(optionsBuilder);
		}
	}
}
