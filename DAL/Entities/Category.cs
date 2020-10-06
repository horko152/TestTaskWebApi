using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
	[Table("categories")]
	public class Category
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

		[JsonIgnore]
		public ICollection<Product> Products { get; set; }
	}
}
