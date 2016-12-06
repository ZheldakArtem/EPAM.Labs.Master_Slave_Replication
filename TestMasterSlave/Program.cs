using MasterSlaveReplication;
using MyServiceLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestMasterSlave
{
    public delegate IList<User> SearchDelegate(params Func<User, bool>[] func);
    class Program
    {
       
        static void Main(string[] args)
        {
            var appDomainSetup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Slave")
            };

            AppDomain domain = AppDomain.CreateDomain("Slave" + 1, null, appDomainSetup);


            var slave = (Slave)domain.CreateInstanceAndUnwrap("MasterSlaveReplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", typeof(Slave).FullName);
        }      
    }
}
