using ServiceLibrary.DbStorage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using ReplicationAPI.Interfaces;
using ReplicationAPI.Services;
using ServiceLibrary;
using ServiceLibrary.Model;

namespace ReplicationAPI.Controllers
{
    public class HomeController : ApiController
    {
		private readonly IMasterService _masterService;

		public HomeController()
	    {
			_masterService = new MasterService();
	    }

	    public IHttpActionResult Initialize()
		{
			for (int i = 0; i < 1000; i++)
				{
					_masterService.Add(new User()
					{
						FirstName = "adadddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadaddddddddddddddddddddddddd" + i,
						DateOfBirth = DateTime.Now,
						LastName = "qwadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddf" + i
					});
				}

			//var d = _masterService.Users.ToList();
			return Ok();
	    }

	    [HttpGet]
	    public IHttpActionResult GetUser(int id)
	    {
		    return Ok(_masterService.GetUserById(id));
	    }

		[HttpDelete]
	    public IHttpActionResult DeleteUser(User user)
	    {
			return Ok(_masterService.Delete(user));
	    }

		[HttpPost]
	    public IHttpActionResult AddU(User user)
	    {
		    if ( _masterService.Add(user)<=0)
		    {
			    return BadRequest();
		    }

		    return Ok();
	    }

	    [HttpPut]
	    public IHttpActionResult Update(User user)
	    {
		    if (!_masterService.Update(user))
		    {
			    return BadRequest();
		    }
		    
		    return Ok();
	    }
    }
}
