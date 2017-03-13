using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.DbStorage
{
	public class DbStorage : IStorage<User>
	{
		private readonly UserDbContext _userContext;

		#region ctor
		public DbStorage(string conStr)
			: this(null, conStr)
		{

		}

		public DbStorage(IList<User> users, string conStr)
		{
			this._userContext = new UserDbContext(conStr);
			if (users != null)
			{
				this.AddRange(users);
			}
		}
		#endregion

		public void AddRange(IList<User> users)
		{
			_userContext.Users.AddRange(users);
			_userContext.SaveChanges();
		}

		public int Add(User user)
		{
			var newUser = _userContext.Users.Add(user);
			_userContext.SaveChanges();

			return newUser.Id;
		}

		public bool Delete(User user)
		{
			var removedUser = _userContext.Users.Remove(user);
			_userContext.SaveChanges();

			return removedUser != null;
		}

		public User GetUserById(int id)
		{
			var user = _userContext.Users.FirstOrDefault(u => u.Id == id);
			_userContext.SaveChanges();

			return user;
		}

		public IList<User> SearchUsers(params Func<User, bool>[] searchCollection)
		{
			var users = GetUsers();

			foreach (var fun in searchCollection)
			{
				users = users.Where(fun).ToList();
			}
			return users;
		}

		public bool UpdateUser(User user)
		{
			var userUp = _userContext.Users.FirstOrDefault(u => u.Id == user.Id);
			if (userUp == null)
			{
				_userContext.Dispose();
				return false;
			}

			userUp.FirstName = user.FirstName;
			userUp.LastName = user.LastName;
			userUp.DateOfBirth = user.DateOfBirth;
			_userContext.SaveChanges();
			return true;
		}

		public IList<User> GetUsers()
		{
			return _userContext.Users.ToList();
		}

		public IEnumerator<User> GetEnumerator()
		{
			return this.GetUsers().GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Dispose()
		{
			_userContext.Dispose();
		}
	}
}
