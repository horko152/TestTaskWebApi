using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskWebApi.Controllers;
using Xunit;

namespace TestTaskWebApi.Tests
{
	public class UserControllerTest
	{
		public UserRepository userRepository;
		public UserControllerTest(UserRepository userRepository)
		{
			this.userRepository = userRepository;
		}
		[Fact]
		public void UsersGetAll()
		{
			var controller = new UserController(userRepository);
			var expected = typeof(IEnumerable<User>);
			var result = controller.GetAll();

			Assert.IsType(expected, result);
		}
	}
}
