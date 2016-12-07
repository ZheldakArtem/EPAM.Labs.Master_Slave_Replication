using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServiceLibrary;
using System.Threading;
using MasterSlaveReplication;
using System.IO;
using System.Reflection;

namespace WcfServiceLibrary
{
    public class SlaveService : ISlaveService
    {

        public static List<Slave> slaves;
        static SlaveService()
        {
            slaves = new List<Slave>();
        }

        public static void Start(int[] ports)
        {
            for (int i = 0; i < ports.Length; i++)
            {
                CreateSlave(ports[i]);
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

            AppDomain domain = AppDomain.CreateDomain($"Slave listen {port} port", null, appDomainSetup);


            var slave = (Slave)domain.CreateInstanceAndUnwrap("MasterSlaveReplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", typeof(Slave).FullName, false, BindingFlags.Default, null, new object[] { port }, null, null);
            new Thread(() => slave.ListenMaster()).Start();
            slaves.Add(slave);
        }

        public IList<User> SearchByLastAndFirstName(UserDataContract userDC)
        {
            int slave = slaves.Count() == 1 ? 0 : new Random().Next(0, slaves.Count() - 1);

            return slaves[slave].SearchByLastAndFirstName(userDC.ToUser());
        }

        public IList<User> SearchByLastName(UserDataContract userDC)
        {
            int slave = slaves.Count() == 1 ? 0 : new Random().Next(0, slaves.Count() - 1);

            return slaves[slave].SearchByLastName(userDC.ToUser());
        }

        public IList<User> SearchByName(UserDataContract userDC)
        {
            int slave = slaves.Count() == 1 ? 0 : new Random().Next(0, slaves.Count() - 1);

            return slaves[slave].SearchByName(userDC.ToUser());
        }
    }
}
