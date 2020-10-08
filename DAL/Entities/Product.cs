using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
	[Table("products")]
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }

		[Column("description")]
		public string Description { get; set; }

		[Required]
		[Column("name")]
		public string Name { get; set; }

		[Column("photo")]
		public string Photo { get; set; }

		[Required]
		[Column("price")]
		public decimal Price { get; set; }

		[Required]
		[Column("quantity")]
		public int Quantity { get; set; }

		//[JsonIgnore]
		[Column("category_id")]
		public int Category_Id { get; set; }

		[ForeignKey("Category_Id")]
		[JsonIgnore]
		public Category Category { get; set; }

		[JsonIgnore]
		public ICollection<Order_Item> Order_Items { get; set; }
	}
}
