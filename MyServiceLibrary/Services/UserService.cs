using System;
using System.Collections.Generic;
using ServiceLibrary.CustomException;
using ServiceLibrary.Interfaces;
using ServiceLibrary.Model;

namespace ServiceLibrary.Services
{
    public class UserService
    {
        private int _lastId;
        private readonly Func<int, int> _increment;
        private readonly IStorage<User> _userStorage;
        private readonly IDumper<User> _dumper;

        #region ctors
		public UserService(string connectedStringName="Master")
			: this(new DbStorage.DbStorage(connectedStringName), s => ++s)
        {
        }

        public UserService(IStorage<User> list) : this(list, s => ++s)
        {
        }

		public UserService(Func<int, int> fun)
			: this(new DbStorage.DbStorage(), fun)
        {
        }

	    private UserService(IStorage<User> list, Func<int, int> inc, IDumper<User> dumper = null)
        {
            this._userStorage = list;
            this._increment = inc;

            foreach (var item in _userStorage)
            {
                item.Id = this.SetUserId();
                this._lastId = item.Id;
            }

            this._dumper = dumper ?? new XmlDump();
        }
        #endregion

        /// <summary>
        /// Method for adding user into a storage
        /// </summary>
        /// <param name="user">user we should to add</param>
        /// <returns>user id</returns>
        public int Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                throw new InvalidUserException();
            }

            user.Id = this.SetUserId();
            this._userStorage.Add(user);
            this._lastId = user.Id;
            return user.Id;
        }

        /// <summary>
        /// Method for removing a user from the storage
        /// </summary>
        /// <param name="user">user we should to remove</param>
        /// <returns>boolean on success</returns>
        public bool Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return this._userStorage.Delete(user);
        }

	    /// <summary>
	    /// Method for getting user by id
	    /// </summary>
	    /// <param name="id">id</param>
	    /// <returns>user</returns>
	    public User GetUserById(int id)
	    {
		    return this._userStorage.GetUserById(id);
	    }

	    /// <summary>
	    /// Method for searching users by name.
	    /// </summary>
	    /// <returns>Collection of users</returns>
	    public IList<User> SearchByName(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return this._userStorage.SearchUsers(u => u.FirstName == user.FirstName);
        }

	    /// <summary>
	    /// Method for searching users by  last name.
	    /// </summary>
	    /// <returns>Collection of users</returns>
	    public IList<User> SearchByLastName(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return this._userStorage.SearchUsers(u => u.LastName == user.LastName);
        }

	    /// <summary>
	    /// Method for searching users by last name and name.
	    /// </summary>
	    /// <returns>Collection of users</returns>
	    public IList<User> SearchByLastAndFirstName(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return this._userStorage.SearchUsers(u => u.LastName == user.LastName, u => u.FirstName == user.FirstName);
        }

        /// <summary>
        /// Method for updating of users in a storage
        /// </summary>
        /// <param name="user"></param>
        /// <returns>boolean on success</returns>
        public bool UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return _userStorage.UpdateUser(user);
        }

	    /// <summary>
	    /// Get all colelction of users
	    /// </summary>
	    /// <returns>Collection users</returns>
	    public IList<User> GetUsers()
	    {
		    return this._userStorage.GetUsers();
	    }

        /// <summary>
        /// The store service state
        /// </summary>
        public void Dump()
        {
            this._dumper.Dump(this._userStorage.GetUsers());
        }

        /// <summary>
        /// Receiving service state which have been stored
        /// </summary>
        /// <returns>Collection of Users</returns>
        public IList<User> GetDump()
        {
            return this._dumper.GetDump();
        }

	    /// <summary>
	    /// Method for generating id.
	    /// </summary>
	    /// <returns>Id</returns>
	    private int SetUserId()
	    {
			return this._lastId == 0 ? this._increment(0) : this._increment(_lastId);
	    }

		public int LastId()
		{
			return _userStorage.LastId();
		}
	}
}
