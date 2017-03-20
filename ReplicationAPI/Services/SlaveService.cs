using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using MasterSlaveReplication;
using ReplicationAPI.Interfaces;
using ServiceLibrary.Model;

namespace ReplicationAPI.Services
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
			try
			{
				var slave=new Slave(port);

				new Thread(() => slave.ListenMaster()).Start();
				Slaves.Add(slave);
				
			}
			catch (Exception ex)
			{
					
				var message=ex.Message;
			}
           
           
        }

        public IList<User> SearchByLastAndFirstName(User userDc)
        {
            int slave = Slaves.Count() == 1 ? 0 : new Random().Next(0, Slaves.Count() - 1);

            return Slaves[slave].SearchByLastAndFirstName(userDc);
        }

		public IList<User> SearchByLastName(User userDc)
        {
            int slave = Slaves.Count() == 1 ? 0 : new Random().Next(0, Slaves.Count() - 1);

            return Slaves[slave].SearchByLastName(userDc);
        }

		public IList<User> SearchByName(User userDc)
        {
            int slave = Slaves.Count() == 1 ? 0 : new Random().Next(0, Slaves.Count() - 1);

            return Slaves[slave].SearchByName(userDc);
        }
	}
}
