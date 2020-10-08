using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
	public class CategoryRepository : GeneralRepository<Category>
	{
		public CategoryRepository(ShopDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}

		public void CreateCategory(Category Entity)
		{
			DbContext.Add(Entity);
			DbContext.SaveChanges();
		}
		public void UpdateCategory(int id, Category Entity)
		{
			var category = DbContext.Categories.FirstOrDefault(x => x.Id == id);
			if (category != null)
			{
				category.Name = Entity.Name;
				category.Description = Entity.Description;
				DbContext.SaveChanges();
			}
			else
			{
				throw new ArgumentException("Object does not exist");
			}
		}
	}
}
