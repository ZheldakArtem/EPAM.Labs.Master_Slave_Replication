using System;
using System.Collections.Generic;

namespace ServiceLibrary.Interfaces
{
    public interface IStorage<T> : IEnumerable<T>
    {
        int Add(T user);

        bool Delete(T user);

        T GetUserById(int id);

        IList<T> SearchUsers(params Func<T, bool>[] searchCollection);

        bool UpdateUser(T user);

        IList<T> GetUsers();
    }
}
