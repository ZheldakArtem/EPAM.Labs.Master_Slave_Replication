using System;
using System.Collections.Generic;
using System.Linq;

namespace MyServiceLibrary
{
    public class UserService
    {
        private int lastId;
        private Func<int, int> increment;
        private IStorage<User> userStorage;
        private IDumper<User> dumper;

        #region ctors
        public UserService() : this(new ListUserStorage(), s => ++s)
        {
        }

        public UserService(IStorage<User> list) : this(list, s => ++s)
        {
        }

        public UserService(Func<int, int> fun) : this(new ListUserStorage(), fun)
        {
        }

        public UserService(IStorage<User> list, Func<int, int> inc, IDumper<User> dumper = null)
        {
            this.userStorage = list;
            this.increment = inc;

            foreach (var item in userStorage)
            {
                item.Id = this.SetUserId();
                this.lastId = item.Id;
            }

            this.dumper = dumper ?? new XmlDump();
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
            this.userStorage.Add(user);
            this.lastId = user.Id;
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

            return this.userStorage.Delete(user);
        }

        /// <summary>
        /// Method for getting user by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>user</returns>
        public User GetUserById(int id) => this.userStorage.GetUserById(id);

        /// <summary>
        /// Method for searching users by name.
        /// </summary>
        /// <param name="funCollection">collection of predicates</param>
        /// <returns>Collection of users</returns>
        public IList<User> SearchByName(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return this.userStorage.SearchUsers(u => u.FirstName == user.FirstName);
        }

        /// <summary>
        /// Method for searching users by  last name.
        /// </summary>
        /// <param name="funCollection">collection of predicates</param>
        /// <returns>Collection of users</returns>
        public IList<User> SearchByLastName(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return this.userStorage.SearchUsers(u => u.LastName == user.LastName);
        }

        /// <summary>
        /// Method for searching users by last name and name.
        /// </summary>
        /// <param name="funCollection">collection of predicates</param>
        /// <returns>Collection of users</returns>
        public IList<User> SearchByLastAndFirstName(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return this.userStorage.SearchUsers(u => u.LastName == user.LastName, u => u.FirstName == user.FirstName);
        }

        /// <summary>
        /// Method for updating of users in a storage
        /// </summary>
        /// <param name="user"></param>
        /// <returns>boolean on success</returns>
        public bool UpdateUser(User user)
        {
            if (user == null || user.Id == 0)
            {
                throw new ArgumentNullException();
            }

            return userStorage.UpdateUser(user);
        }

        /// <summary>
        /// Get all colelction of users
        /// </summary>
        /// <returns>Collection users</returns>
        public IList<User> GetUsers() => this.userStorage.GetUsers();

        /// <summary>
        /// The store service state
        /// </summary>
        public void Dump()
        {
            this.dumper.Dump(this.userStorage.GetUsers());
        }

        /// <summary>
        /// Receiving service state which have been stored
        /// </summary>
        /// <returns>Collection of Users</returns>
        public IList<User> GetDump()
        {
            return this.dumper.GetDump();
        }

        /// <summary>
        /// Method for generating id.
        /// </summary>
        /// <returns>Id</returns>
        private int SetUserId() => this.lastId == 0 ? this.increment(0) : this.increment(lastId);
    }
}
