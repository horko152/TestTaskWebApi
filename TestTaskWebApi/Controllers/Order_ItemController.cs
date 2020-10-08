using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestTaskWebApi.Controllers
{
	[Route("api/order_item")]
	[ApiController]
	public class OrderItemController : ControllerBase
	{
		private readonly Order_ItemRepository order_itemRepository;

		public OrderItemController(Order_ItemRepository order_itemRepository)
		{
			this.order_itemRepository = order_itemRepository;
		}

		///<summary>
		///Get All Order_Items
		///</summary>
		[HttpGet]
		[Route("~/api/order_items")]
		public async Task<ActionResult<IEnumerable<Order_Item>>> GetAll()
		{
			return await order_itemRepository.GetAll().ToListAsync();
		}

		///<summary>
		///Get Order_Items
		///</summary>
		[HttpGet("{id:int}")]
		public ActionResult<Order_Item> GetOrderItem([FromRoute] int id)
		{
			var order_item = order_itemRepository.GetById(id);
			if (order_item == null)
			{
				return NotFound();
			}

			return order_item;
		}

		// POST: api/OrderItem
		///<summary>
		///Create OrderItem
		///</summary>
		/// <param name="item"></param>
		/// <response code="200">OK</response>
		/// <response code="201">Item created</response>
		/// <response code="400">If the item is null</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Order_Item> CreateOrderItem(Order_Item order_item)
		{
			order_itemRepository.CreateOrderItem(order_item);
			return CreatedAtAction("GetOrderItem", new { id = order_item.Id }, order_item);
		}

		///<summary>
		///Update Order_Item
		///</summary>
		[HttpPut("{id}")]
		public ActionResult UpdateOrderItem([FromRoute] int id, [FromBody] Order_Item order_item)
		{
			try
			{
				order_itemRepository.UpdateOrderItem(id, order_item);
				return Ok();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderItemExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
		}

		///<summary>
		///Delete Order_Item
		///</summary>
		[HttpDelete("{id}")]
		public ActionResult Delete([FromRoute] int id)
		{
			int res = order_itemRepository.Delete(id);
			if (res != 0)
			{
				return Ok();
			}
			return NotFound();
		}
		private bool OrderItemExists(int id)
		{
			return order_itemRepository.GetAll().Any(e => e.Id == id);
		}
	}

}
