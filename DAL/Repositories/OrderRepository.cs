using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
	public class OrderRepository : GeneralRepository<Order>
	{
		public OrderRepository(ShopDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}
		public int CreateOrder(Order Entity)
		{
			DbContext.Add(Entity);
			DbContext.SaveChanges();
			return Entity.Id;
		}
		public IQueryable<Order> GetOrderByUserId(int id)
		{
			IQueryable<Order> orders = DbContext.Orders.ToList().Where(x => x.User_Id == id).AsQueryable();
			if (orders != null)
			{
				return orders;
			}
			else
			{
				throw new ArgumentException();
			}
		}
		public void UpdateOrder(int id, Order Entity)
		{
			var order = DbContext.Orders.FirstOrDefault(x => x.Id == id);
			if (order != null)
			{
				order.Comment = Entity.Comment;
				order.User_Id = Entity.User_Id;
				DbContext.SaveChanges();
			}
			else
			{
				throw new ArgumentException();
			}
		}
	}
}
