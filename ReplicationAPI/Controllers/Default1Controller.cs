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
    public class Default1Controller : ApiController
    {
		private readonly IMasterService _masterService;

		public Default1Controller()
	    {
			_masterService = new MasterService();
	    }

		[HttpGet]
	    public IHttpActionResult Index()
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
    }
}
