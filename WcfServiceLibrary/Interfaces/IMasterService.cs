using MyServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMasterService
    {
        [OperationContract]
        int Add(UserDataContract user);

        [OperationContract]
        bool Update(UserDataContract user);

        [OperationContract]
        bool Delete(UserDataContract user);

        [OperationContract]
        User GetUserById(int id);

        [OperationContract]
        IList<User> SearchByName(UserDataContract user);

        [OperationContract]
        IList<User> SearchByLastName(UserDataContract user);

        [OperationContract]
        IList<User> SearchByLastAndFirstName(UserDataContract user);

        [OperationContract]
        IList<User> GetUsers();
        
    }

    [DataContract]
    public class UserDataContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public VisaDataContract UserVisa { get; set; }

        [DataMember]
        public Gender UserGender { get; set; }
    }

    [DataContract]
    public struct VisaDataContract
    {
        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }

    }
   

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfServiceLibrary.ContractType".

}
