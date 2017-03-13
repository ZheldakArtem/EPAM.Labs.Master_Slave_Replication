using System;
using System.Collections.Generic;

namespace MyServiceLibrary
{
    public enum Gender { Male, Famale }

    [Serializable]
    public class User : IEqualityComparer<User>, IEquatable<User>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Visa UserVisa { get; set; }

        public Gender UserGender { get; set; }

        public bool Equals(User x, User y)
        {
            if (x == y)
            {
                return true;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            if (x.Id == y.Id && x.FirstName == y.FirstName && x.LastName == y.LastName && x.DateOfBirth == y.DateOfBirth)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(User user)
        {
            return user.Id.GetHashCode() + user.FirstName.GetHashCode() + user.FirstName.GetHashCode();
        }

        public bool Equals(User user)
        {
            if (user == null)
            {
                return false;
            }

            return Equals(this, user);
        }

        public override string ToString()
        {
			return string.Format("Name: {0} \n Sername: {1} \n Date Of Birthday: {2}", this.FirstName, this.LastName, this.DateOfBirth);
        }
    }
}
