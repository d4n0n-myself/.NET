using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using September26.Dto;
using September26.Models;

namespace September26.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			var accounts = new[]
			{
				new User
				{
					FirstName = "Danel",
					LastName = "Sibaev",
					Email = "retartd2@gmail.com",
					Organisation = new Organisation(){ Name = "Hoes"}
				},
				new User
				{
					FirstName = "Anton",
					LastName = "Krylov",
					Email = "retardalert@gmail.com",
					Organisation = new Organisation(){Name="Retards"}
				}
			};

//			Expression<Func<User, UserDto>> expression = x => new UserDto
//			{
//				Name = "Mr_" + x.Name + x.LastName,
//				Email = x.Email
//			};

//			var dtos = accounts.AsQueryable()
//				.ProjectTo<UserDto>(Startup.MapperConfiguration)
//				.ToList();
			
			return View();
		}

		private void GetExpression()
		{
			var constrInfo = Expression.New(typeof(User).GetConstructors().Single());
			var lambda = Expression.Lambda<User>(constrInfo,  Expression.Parameter(typeof(UserDto)));
			
			var nameUserDto = typeof(UserDto).GetProperty("Name");
			var emailuserDto = typeof(UserDto).GetProperty("Email");
			var emailuser = typeof(User).GetProperty("Email");
			var nameUser = typeof(User).GetProperty("Name");
			var lastNameUser = typeof(User).GetProperty("LastName");
			
			var concatMethodInfo = typeof(string)
				.GetMethods(BindingFlags.Public | BindingFlags.Static)
				.First(methodInfo => methodInfo.Name == "Concat");

			var methodCallExpression = Expression.Call(concatMethodInfo,
				Expression.Constant("Mr. "),
				Expression.Parameter(typeof(string), "FirstName"),
				Expression.Constant(" "),
				Expression.Parameter(typeof(string), "LastName"));
		}

		public IActionResult Privacy()
		{
			return View(new User()
			{
				FirstName = "Hello", 
				LastName = "its me merio",
				Organisation = new Organisation() {Name = "SuperOrg", Email = "superorg@gmaiol.com"}
			});
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
		}
	}
}