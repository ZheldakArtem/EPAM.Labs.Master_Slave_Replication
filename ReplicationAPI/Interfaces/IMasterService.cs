using System.Collections.Generic;
using ServiceLibrary.Model;

namespace ReplicationAPI.Interfaces
{
	public interface IMasterService
	{
		int Add(User user);

		bool Update(User user);

		bool Delete(User user);

		User GetUserById(int id);

		IList<User> SearchByName(User user);

		IList<User> SearchByLastName(User user);

		IList<User> SearchByLastAndFirstName(User user);

		IList<User> GetUsers();
	}
}
