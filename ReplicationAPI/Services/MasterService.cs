using System.Collections.Generic;
using System.Configuration;
using MasterSlaveReplication;
using ReplicationAPI.CustomSections;
using ReplicationAPI.Interfaces;
using ServiceLibrary.Model;

namespace ReplicationAPI.Services
{
    public class MasterService : IMasterService
    {
        private static readonly Master Master;
        static MasterService()
        {
			//Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			//var sections = cfg.Sections;
			//var settings = (CustomConfigSection)sections["initialSettings"];
			var ports = new[] { 243, 2332, 2333 };//settings.ServiceNodesItems.ToArray();
            SlaveService.Start(ports);
            Master = new Master(ports);
        }

		public int Add(User userDc)
	    {
		    return  Master.Add(userDc);
	    }

		public bool Delete(User userDc)
	    {
		    return Master.Delete(userDc);
	    }

	    public User GetUserById(int id)
	    {
		    return Master.GetUserById(id);
	    }

	    public IList<User> GetUsers()
	    {
		    return Master.GetUsers();
	    }

		public IList<User> SearchByLastAndFirstName(User userDc)
	    {
		    return Master.SearchByLastAndFirstName(userDc);
	    }

		public IList<User> SearchByLastName(User userDc)
	    {
		    return Master.SearchByLastName(userDc);
	    }

		public IList<User> SearchByName(User userDc)
	    {
		    return Master.SearchByName(userDc);
	    }

		public bool Update(User userDc)
	    {
		    return Master.Update(userDc);
	    }
    }
}
