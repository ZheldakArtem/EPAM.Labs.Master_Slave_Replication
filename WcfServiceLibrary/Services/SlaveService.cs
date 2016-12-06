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
        public static bool toggle;
        public static readonly int numberSlaves;
        public static List<Slave> slaves;
        static SlaveService()
        {
            numberSlaves = 5;

            slaves = new List<Slave>();
            Init();
        }

        private static void Init()
        {
            for (int i = 1; i <= 5; i++)
            {
                CreateSlave(i);
            }
        }

        private static void CreateSlave(int i)
        {

            var appDomainSetup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Slave")
            };

            AppDomain domain = AppDomain.CreateDomain("Slave" + i, null, appDomainSetup);


            var slave = (Slave)domain.CreateInstanceAndUnwrap("MasterSlaveReplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", typeof(Slave).FullName, false, BindingFlags.Default, null, new object[] { 288 + i }, null, null);
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
