using ServiceLibrary.DbStorage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceLibrary;
using ServiceLibrary.Model;

namespace ReplicationAPI.Controllers
{
    public class Default1Controller : ApiController
    {
		private readonly UserDbContext _context;
	    public Default1Controller()
	    {
			
			_context = new UserDbContext("Slave_1");
			_context.Users.Add(new User()
			 {
				 FirstName = "Artem",
				 LastName = "Zheldak",
				 DateOfBirth = DateTime.Now,
				 UserVisa = new Visa() { Country = "Nigeria"}
			 });
			_context.Users.Add(new User()
			{
				FirstName = "Artem123214",
				LastName = "Zheldak",
				DateOfBirth = DateTime.Now,
				UserVisa = new Visa() { Country = "USA"}
				
			});
			_context.SaveChanges();
	    }

		[HttpGet]
	    public IHttpActionResult Index()
		{
			
			var d = _context.Users.ToList();
			return Ok(d);
	    }
    }
}
