using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
	public class Order_ItemRepository : GeneralRepository<Order_Item>
	{
		public Order_ItemRepository(ShopDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}
		public string CreateOrderItem(Order_Item Entity)
		{
			DbContext.Add(Entity);
			DbContext.SaveChanges();
			return "Success";
		}
		public IQueryable<Order_Item> GetOrderItemByOrderId(int id)
		{
			IQueryable<Order_Item> order_items = DbContext.Order_Items.ToList().Where(x => x.Order_Id == id).AsQueryable();
			if (order_items != null)
			{
				return order_items;
			}
			else
			{
				throw new ArgumentException();
			}
		}
		public void UpdateOrderItem(int id, Order_Item Entity)
		{
			var order_item = DbContext.Order_Items.FirstOrDefault(x => x.Id == id);
			//HttpResponseMessage responce = null;
			if (order_item != null)
			{
				order_item.Quantity = Entity.Quantity;
				order_item.Product_Id = Entity.Product_Id;
				DbContext.SaveChanges();
				//return responce = new HttpResponseMessage(HttpStatusCode.OK);
			}
			else
			{
				throw new ArgumentException();
			}
			//else return responce = new HttpResponseMessage(HttpStatusCode.NotFound);
		}
	}
}
