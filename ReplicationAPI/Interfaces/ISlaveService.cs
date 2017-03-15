using System.Collections.Generic;
using ServiceLibrary.Model;

namespace ReplicationAPI.Interfaces
{
	public interface ISlaveService
	{
		IList<User> SearchByName(User user);

		IList<User> SearchByLastName(User user);

		IList<User> SearchByLastAndFirstName(User user);
	}
}