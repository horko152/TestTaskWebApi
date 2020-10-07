using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DAL.Repositories
{
	public class ProductRepository : GeneralRepository<Product>
	{
		public ProductRepository(ShopDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}

		public void CreateProduct(Product Entity)
		{
			DbContext.Add(Entity);
			DbContext.SaveChanges();
		}
		public IQueryable<Product> GetProductsByCategory(int id)
		{
			IQueryable<Product> products = DbContext.Products.ToList().Where(x => x.Category_Id == id).AsQueryable();
			if(products != null)
			{
				return products;
			}
			else
			{
				throw new ArgumentException();
			}
		}
		public void UpdateProduct(int id, Product Entity)
		{
			var product = DbContext.Products.FirstOrDefault(x => x.Id == id);
			if(product != null)
			{
				product.Name = Entity.Name;
				product.Description = Entity.Description;
				product.Quantity = Entity.Quantity;
				product.Price = Entity.Price;
				DbContext.SaveChanges();
			}
			else
			{
				throw new ArgumentException();
			}
		}

	}
}
