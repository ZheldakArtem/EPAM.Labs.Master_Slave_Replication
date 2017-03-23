using System.Collections.Generic;
using ServiceLibrary.Model;

namespace ReplicationAPI.Interfaces
{
	public interface ISlaveService
	{
		User SearchById(int id);

		IList<User> SearchByName(User user);

		IList<User> SearchByLastName(User user);

		IList<User> SearchByLastAndFirstName(User user);

		int LastId();
	}
}