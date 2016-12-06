using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MyServiceLibrary;
using MasterSlaveReplication;
using System.IO;

namespace WcfServiceLibrary
{
    public class MasterService : IMasterService
    {
        private static readonly Master master;
        static MasterService()
        {
            SlaveService.toggle = true;
                        master = new Master(5);
        }

        public int Add(UserDataContract userDC) => master.Add(userDC.ToUser());

        public bool Delete(UserDataContract userDC) => master.Delete(userDC.ToUser());

        public User GetUserById(int id) => master.GetUserById(id);

        public IList<User> GetUsers() => master.GetUsers();

        public IList<User> SearchByLastAndFirstName(UserDataContract userDC) => master.SearchByLastAndFirstName(userDC.ToUser());


        public IList<User> SearchByLastName(UserDataContract userDC) => master.SearchByLastName(userDC.ToUser());


        public IList<User> SearchByName(UserDataContract userDC) => master.SearchByName(userDC.ToUser());

        public bool Update(UserDataContract userDC) => master.Update(userDC.ToUser());

    }
}
