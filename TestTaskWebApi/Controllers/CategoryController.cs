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
	[Route("api/category")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly CategoryRepository categoryRepository;

		public CategoryController(CategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		// GET: api/Categories
		///<summary>
		///Get All Categories
		///</summary>
		[HttpGet]
		[Route("~/api/categories")]
		public async Task<ActionResult<IEnumerable<Category>>> GetAll()
		{
			return await categoryRepository.GetAll().ToListAsync();
		}

		// GET: api/Category/5
		///<summary>
		///Get CAtegory By CategoryId
		///</summary>
		[HttpGet("{id:int}")]
		public ActionResult<Category> GetCategory([FromRoute] int id)
		{
			var category = categoryRepository.GetById(id);
			if (category == null)
			{
				return NotFound();
			}

			return category;
		}

		// PUT: api/Genre/5
		///<summary>
		///Update Genre
		///</summary>
		[HttpPut("{id}")]
		public IActionResult UpdateCategory(int id, Category category)
		{
			if (id != category.Id)
			{
				return BadRequest();
			}
			try
			{
				categoryRepository.UpdateCategory(id, category);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoryExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Category
		///<summary>
		///Create Category
		///</summary>
		/// <param name="item"></param>
		/// <response code="200">OK</response>
		/// <response code="201">Item created</response>
		/// <response code="400">If the item is null</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Category> CreateCategory(Category category)
		{
			categoryRepository.CreateCategory(category);
			return CreatedAtAction("GetCategory", new { id = category.Id }, category);
		}

		// DELETE: api/Category/5
		///<summary>
		///Delete Category
		///</summary>
		/// <param name="id"></param> 
		[HttpDelete("{id}")]
		public void DeleteCategory(int id)
		{
			categoryRepository.Delete(id);
		}

		private bool CategoryExists(int id)
		{
			return categoryRepository.GetAll().Any(e => e.Id == id);
		}
	}
}
