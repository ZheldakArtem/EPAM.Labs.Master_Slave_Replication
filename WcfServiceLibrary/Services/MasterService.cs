using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ServiceLibrary;
using MasterSlaveReplication;
using System.IO;
using System.Configuration;
using ServiceLibrary.Model;
using WcfServiceLibrary.Services;

namespace WcfServiceLibrary
{
    public class MasterService : IMasterService
    {
        private static readonly Master Master;
        static MasterService()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var sections = cfg.Sections;
            var settings = (CustomConfigSection)sections["initialSettings"];
            var ports = settings.ServiceNodesItems.ToArray();
            SlaveService.Start(ports);
            Master = new Master(ports);
        }

	    public int Add(UserDataContract userDC)
	    {
		    return  Master.Add(userDC.ToUser());
	    }

	    public bool Delete(UserDataContract userDC)
	    {
		    return Master.Delete(userDC.ToUser());
	    }

	    public User GetUserById(int id)
	    {
		    return Master.GetUserById(id);
	    }

	    public IList<User> GetUsers()
	    {
		    return Master.GetUsers();
	    }

	    public IList<User> SearchByLastAndFirstName(UserDataContract userDC)
	    {
		    return Master.SearchByLastAndFirstName(userDC.ToUser());
	    }

	    public IList<User> SearchByLastName(UserDataContract userDC)
	    {
		    return Master.SearchByLastName(userDC.ToUser());
	    }

	    public IList<User> SearchByName(UserDataContract userDC)
	    {
		    return Master.SearchByName(userDC.ToUser());
	    }

	    public bool Update(UserDataContract userDC)
	    {
		    return Master.Update(userDC.ToUser());
	    }
    }
}
