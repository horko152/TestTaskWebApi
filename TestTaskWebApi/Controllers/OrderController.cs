using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace TestTaskWebApi.Controllers
{
	[Route("api/order")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly OrderRepository orderRepository;
		public OrderController(OrderRepository orderRepository)
		{
			this.orderRepository = orderRepository;
		}

		///<summary>
		///Get All Orders
		///</summary>
		[HttpGet]
		[Route("~/api/orders")]
		public async Task<ActionResult<IEnumerable<Order>>> GetAll()
		{
			return await orderRepository.GetAll().ToListAsync();
		}

		///<summary>
		///Get Order By Id
		///</summary>
		[HttpGet("{id:int}")]
		public ActionResult<Order> GetOrder([FromRoute] int id)
		{
			var order = orderRepository.GetById(id);
			if (order == null)
			{
				return NotFound();
			}

			return order;
		}

		/////<summary>
		/////Get Orders By UserId
		/////</summary>
		//[HttpGet("{id:int}")]
		//public IQueryable<Order> GetOrderByUserId([FromRoute] int id)
		//{
		//	return orderRepository.GetOrderByUserId(id);
		//}

		// POST: api/Order
		///<summary>
		///Create Order
		///</summary>
		/// <param name="item"></param>
		/// <response code="200">OK</response>
		/// <response code="201">Item created</response>
		/// <response code="400">If the item is null</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Order> CreateOrder(Order order)
		{
			orderRepository.CreateOrder(order);
			return CreatedAtAction("GetOrder", new { id = order.Id }, order);
		}


		///<summary>
		///Update Order
		///</summary>
		[HttpPut("{id}")]
		public ActionResult UpdateOrder([FromRoute] int id, [FromBody] Order order)
		{
			try
			{
				orderRepository.UpdateOrder(id, order);
				return Ok();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderExists(id))
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
		///Delete Order
		///</summary>
		/// <param name="id"></param> 
		[HttpDelete("{id}")]
		public ActionResult Delete([FromRoute] int id)
		{
			int res = orderRepository.Delete(id);
			if (res != 0)
			{
				return Ok();
			}
			return NotFound();
		}
		private bool OrderExists(int id)
		{
			return orderRepository.GetAll().Any(e => e.Id == id);
		}
	}
}
