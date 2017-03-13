using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary
{
    [Serializable]
    public class ListUserStorage : IStorage<User>
    {
        private IList<User> _listUsers;

        #region ctors
        public ListUserStorage()
        {
            this._listUsers = new List<User>();
        }

        public ListUserStorage(IList<User> users)
        {
            _listUsers = users;
        }

        #endregion

        public int Add(User user)
        {
            _listUsers.Add(user);
            return user.Id;
        }

	    public bool Delete(User user)
	    {
		    return _listUsers.Remove(user);
	    }


        public IEnumerator<User> GetEnumerator()
        {
            return this._listUsers.GetEnumerator();
        }

	    public User GetUserById(int id)
	    {
			return	this._listUsers.FirstOrDefault(u => u.Id == id);
	    }


	    public IList<User> GetUsers()
	    {
		    return _listUsers;
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
            User userUp = this.GetUserById(user.Id);
            if (userUp == null)
            {
                return false;
            }

            userUp.FirstName = user.FirstName;
            userUp.LastName = user.LastName;
            userUp.DateOfBirth = user.DateOfBirth;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
