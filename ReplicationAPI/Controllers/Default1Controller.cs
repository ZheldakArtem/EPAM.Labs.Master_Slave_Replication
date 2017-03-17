using ServiceLibrary.DbStorage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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
			
			//var d = _masterService.Users.ToList();
			return Ok();
	    }
    }
}
