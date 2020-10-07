using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
	[Table("order_items")]
	public class Order_Item
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")] 
		public int Id { get; set; }

		[Required]
		[Column("quantity")]
		public int Quantity { get; set; }

		//[JsonIgnore]
		[Column("order_id")]
		public int Order_Id { get; set; }

		[ForeignKey("Order_Id")]
		[JsonIgnore]
		public Order Order { get; set; }

		//[JsonIgnore]
		[Column("product_id")]
		public int Product_Id { get; set; }

		[ForeignKey("Product_Id")]
		[JsonIgnore]
		public Product Product { get; set; }
	}
}
