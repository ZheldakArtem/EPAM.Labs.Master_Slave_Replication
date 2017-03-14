using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLibrary.Model;

namespace WcfServiceLibrary
{
   public static class Mapper
    {
        public static User ToUser(this UserDataContract userDc)
        {
            return new User()
            {
                Id = userDc.Id,
                FirstName = userDc.FirstName,
                LastName = userDc.LastName,
                DateOfBirth = userDc.DateOfBirth,
            };
        }
    }
}
