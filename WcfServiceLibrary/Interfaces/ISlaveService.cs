using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceLibrary.Model;

namespace WcfServiceLibrary
{
    [ServiceContract]
    public interface ISlaveService
    {
        [OperationContract]
        IList<User> SearchByName(UserDataContract user);

        [OperationContract]
        IList<User> SearchByLastName(UserDataContract user);

        [OperationContract]
        IList<User> SearchByLastAndFirstName(UserDataContract user);
    }
}
