using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using MasterSlaveReplication;
using ServiceLibrary;
using ServiceLibrary.Model;

namespace WcfServiceLibrary.Services
{
    public class SlaveService : ISlaveService
    {
	    private static readonly List<Slave> Slaves;
        static SlaveService()
        {
            Slaves = new List<Slave>();
        }

        public static void Start(int[] ports)
        {
	        foreach (int p in ports)
	        {
		        CreateSlave(p);
	        }
        }
        private static void Init()
        {

        }

        private static void CreateSlave(int port)
        {

            var appDomainSetup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Slave")
            };

            AppDomain domain = AppDomain.CreateDomain(string.Format("Slave listen {0} port",port), null, appDomainSetup);


            var slave = (Slave)domain.CreateInstanceAndUnwrap("MasterSlaveReplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", typeof(Slave).FullName, false, BindingFlags.Default, null, new object[] { port }, null, null);
            new Thread(() => slave.ListenMaster()).Start();
            Slaves.Add(slave);
        }

        public IList<User> SearchByLastAndFirstName(UserDataContract userDc)
        {
            int slave = Slaves.Count() == 1 ? 0 : new Random().Next(0, Slaves.Count() - 1);

            return Slaves[slave].SearchByLastAndFirstName(userDc.ToUser());
        }

        public IList<User> SearchByLastName(UserDataContract userDc)
        {
            int slave = Slaves.Count() == 1 ? 0 : new Random().Next(0, Slaves.Count() - 1);

            return Slaves[slave].SearchByLastName(userDc.ToUser());
        }

        public IList<User> SearchByName(UserDataContract userDc)
        {
            int slave = Slaves.Count() == 1 ? 0 : new Random().Next(0, Slaves.Count() - 1);

            return Slaves[slave].SearchByName(userDc.ToUser());
        }
    }
}
