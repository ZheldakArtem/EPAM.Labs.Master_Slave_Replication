using MyServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
   public static class Mapper
    {
        public static User ToUser(this UserDataContract userDC)
        {
            return new User()
            {
                Id = userDC.Id,
                FirstName = userDC.FirstName,
                LastName = userDC.LastName,
                DateOfBirth = userDC.DateOfBirth,
                UserGender = userDC.UserGender,
                UserVisa = userDC.UserVisa.ToVisa()
            };
        }

        public static Visa ToVisa(this VisaDataContract visa)
        {
            return new Visa()
            {
                Country =visa.Country,
                Start=visa.Start,
                End=visa.End
            };
        }
    }
}
