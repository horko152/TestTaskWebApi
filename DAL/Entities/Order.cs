using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
	[Table("orders")]
	public class Order
	{
		public Order()
		{
			Created_At = DateTime.Now;
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }

		[Required]
		[Column("created_at")]
		public DateTime Created_At { get; set; }

		[Column("comment")]
		public string Comment { get; set; }

		//[JsonIgnore]
		[Column("user_id")]
		public int User_Id { get; set; }

		[ForeignKey("User_Id")]
		[JsonIgnore]
		public User User { get; set; }

		[JsonIgnore]
		public ICollection<Order_Item> Order_Items { get; set; }
	}
}
