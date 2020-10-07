using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
	public class UserRepository : GeneralRepository<User>
	{
		public UserRepository(ShopDbContext DbContext) : base(DbContext)
		{
			this.DbContext = DbContext;
		}

		public void CreateUser(User Entity)
		{
			DbContext.Add(Entity);
			DbContext.SaveChanges();
		}
		public void UpdateUser(int id, User Entity)
		{
			var user = DbContext.Users.FirstOrDefault(x => x.Id == id);
			if (user != null)
			{
				user.First_Name = Entity.First_Name;
				user.Last_Name = Entity.Last_Name;
				user.UserName = Entity.UserName;
				user.Email = Entity.Email;
				user.Password = Entity.Password;
				user.Created_At = Entity.Created_At;
				user.Active = Entity.Active;
				user.Role = Entity.Role;
				DbContext.SaveChanges();
			}
			else
			{
				throw new ArgumentException();
			}
		}
	}
}
