using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
	public class ShopDbContext : DbContext
	{

		public ShopDbContext(){}
		public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
		public DbSet<User> Users { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Order_Item> Order_Items { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(@"Server=DESKTOP-7I2GP9S;Database=ShopDb;Trusted_Connection=True;");
			}
		}

	}
}
