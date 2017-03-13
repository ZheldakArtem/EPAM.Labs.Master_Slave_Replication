using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;

namespace MyServiceLibrary.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private List<User> users = new List<User>()
        {
             new User()
            {
                FirstName = "Artem",
                LastName = "Pupin",
                DateOfBirth = DateTime.Now
            },
             new User()
             {
                 FirstName = "Artem",
                 LastName = "Zheldak",
                 DateOfBirth = DateTime.Now
            },
             new User()
            {
                FirstName = "Dima",
                LastName = "DDD",
                DateOfBirth = DateTime.Now
            },
             new User()
            {
                FirstName = "Pasha",
                LastName = "DDD",
                DateOfBirth = DateTime.Now
            },
             new User()
             {
                FirstName = "Lesha",
                LastName = "Zheldak",
                DateOfBirth = DateTime.Now
             }
    };
        #region tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullUser_ExceptionThrown()
        {
            var service = new UserService();

            service.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserException))]
        public void Add_EmptyFirstName_ExceptionThrown()
        {
            var service = new UserService();

            service.Add(new User()
            {
                FirstName = string.Empty,
                LastName = "Ivanov",
                DateOfBirth = DateTime.Now
            });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserException))]
        public void Add_EmptylastName_ExceptionThrown()
        {
            var service = new UserService();

            service.Add(new User()
            {
                FirstName = "Ivan",
                LastName = string.Empty,
                DateOfBirth = DateTime.Now
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullUser_ExeptionTHrown()
        {
            var service = new UserService();

            service.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUsers_NullPredicate_ExceptionThrown()
        {
            var service = new UserService();

            service.SearchByName(null);
        }

        [TestMethod]
        public void Add_ValidUser()
        {
            var service = new UserService(); ////default increment +1
            var user = new User()
            {
                FirstName = "Artem",
                LastName = "Zheldak",
                DateOfBirth = DateTime.Now
            };
            service.Add(user);
            Assert.AreEqual(1, user.Id);
        }

        [TestMethod]
        public void Add_ValidUserNonDefaultIncrement()
        {
            var service = new UserService(s => s + 2);
            var user = new User()
            {
                FirstName = "Artem",
                LastName = "Zheldak",
                DateOfBirth = DateTime.Now
            };

            Assert.AreEqual(2, service.Add(user));
        }

        [TestMethod]
        public void Delete_User()
        {
            var service = new UserService();
            var user = new User()
            {
                FirstName = "Artem",
                LastName = "Zheldak",
                DateOfBirth = DateTime.Now
            };
            service.Add(user);

            Assert.IsTrue(service.Delete(user));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullUser_ExceptionThrown()
        {
            var service = new UserService();

            service.UpdateUser(null);
        }

        [TestMethod]
        public void Update_ValidUser()
        {
            var service = new UserService();
            var oldUser = new User()
            {
                FirstName = "Artem",
                LastName = "Zheldak",
                DateOfBirth = DateTime.Now
            };

            var newfirstUser = new User()
            {
                FirstName = "Artem",
                LastName = "Petrov",
                DateOfBirth = DateTime.Now
            };

            var newSecondUser = new User()
            {
                Id = 1,
                FirstName = "Artem",
                LastName = "Petrov",
                DateOfBirth = DateTime.Now
            };
            service.Add(oldUser);

            Assert.IsTrue(!service.UpdateUser(newfirstUser));
            Assert.IsTrue(service.UpdateUser(newSecondUser));
        }

        [TestMethod]
        public void SearchUserByFName()
        {
            var service = new UserService(new ListUserStorage(users));
            var artemName = service.SearchByName(new User()
            {
                FirstName = "Artem",
                LastName = "Pupin",
                DateOfBirth = DateTime.Now
            });
            var dimaName = service.SearchByName(new User()
            {
                FirstName = "Dima",
                LastName = "Pupin",
                DateOfBirth = DateTime.Now
            });

            Assert.AreEqual(2, artemName.Count());
            Assert.AreEqual(1, dimaName.Count());
        }

        [TestMethod]
        public void SearchUserByFNameAndLName()
        {
            var service = new UserService(new ListUserStorage(users));
            var artemFNameLName = service.SearchByLastAndFirstName(new User()
            {
                FirstName = "Artem",
                LastName = "Pupin",
                DateOfBirth = DateTime.Now
            });

            Assert.AreEqual(1, artemFNameLName.Count());
        }

        [TestMethod]
        public void SerializationAndDeserialization()
        {
            var service = new UserService(new ListUserStorage(users));
            IList<User> usersFromCollection = service.GetUsers();
            service.Dump();
            IList<User> usersFromXml = service.GetDump();
            Assert.AreEqual(usersFromCollection.Count(), usersFromXml.Count());
        }

        [TestMethod]
        public void TestAppConfig()
        {
            var dumper = new XmlDump();
        }
        #endregion

        [TestMethod]
        public void TEstSlaveMaster()
        {

        }
    }
}
