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
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ProductRepository productRepository;

		public ProductController(ProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		// GET: api/Products
		///<summary>
		///Get All Products
		///</summary>
		[HttpGet]
		[Route("~/api/products")]
		public async Task<ActionResult<IEnumerable<Product>>> GetAll()
		{
			return await productRepository.GetAll().ToListAsync();
		}

		// GET: api/Product/5
		///<summary>
		///Get Product By ProductId
		///</summary>
		[HttpGet("{id:int}")]
		public ActionResult<Product> GetProduct([FromRoute] int id)
		{
			var product = productRepository.GetById(id);
			if (product == null)
			{
				return NotFound();
			}

			return product;
		}

		// PUT: api/Product/5
		///<summary>
		///Update Product
		///</summary>
		[HttpPut("{id}")]
		public ActionResult UpdateProduct(int id, Product product)
		{
			try
			{
				productRepository.UpdateProduct(id, product);
				return Ok();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
		}

		// POST: api/Product
		///<summary>
		///Create Product
		///</summary>
		/// <param name="item"></param>
		/// <response code="200">OK</response>
		/// <response code="201">Item created</response>
		/// <response code="400">If the item is null</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Product> CreateProduct(Product product)
		{
			productRepository.CreateProduct(product);
			return CreatedAtAction("GetProduct", new { id = product.Id }, product);
		}

		// DELETE: api/Product/5
		///<summary>
		///Delete Product
		///</summary>
		/// <param name="id"></param> 
		[HttpDelete("{id}")]
		public ActionResult DeleteProduct(int id)
		{
			int res = productRepository.Delete(id);
			if (res != 0)
			{
				return Ok();
			}
			return NotFound();
		}

		private bool ProductExists(int id)
		{
			return productRepository.GetAll().Any(e => e.Id == id);
		}
	}
}
