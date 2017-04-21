using System;
using System.Web.Http;
using ReplicationAPI.Interfaces;
using ReplicationAPI.Services;
using ServiceLibrary.Model;
using System.Threading.Tasks;

namespace ReplicationAPI.Controllers
{
	public class HomeController : ApiController
	{
		private readonly IMasterService _masterService;
		private readonly ISlaveService _slaveService;

		public HomeController()
		{
			_masterService = new MasterService();
			_slaveService = new SlaveService();
		}

		//[HttpGet]
		public IHttpActionResult Initialize()
		{
				for (int i = 0; i < 2000; i++)
				{
					_masterService.Add(new User()
					{
						FirstName = "adadddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadaddddddddddddddddddddddddd" + i,
						DateOfBirth = DateTime.Now,
						LastName = "qwadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddadadddddddddddddddddddddddf" + i
					});
			}

			return Ok();
		}

		[HttpGet]
		public IHttpActionResult GetUserMaster(int id)
		{
			return Ok(_masterService.GetUserById(id));
		}

		[HttpGet]
		public IHttpActionResult GetUserSlaves(int id)
		{
			return Ok(_slaveService.SearchById(id));
		}

		[HttpDelete]
		public IHttpActionResult DeleteUser(User user)
		{
			return Ok(_masterService.Delete(user));
		}

		[HttpPost]
		public IHttpActionResult AddU(User user)
		{
			if (_masterService.Add(user) <= 0)
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

		[HttpGet]
		public IHttpActionResult GetMaxId()
		{
			return Ok(_slaveService.LastId());
		}
	}
}
