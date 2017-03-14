using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    public interface IDumper<T>
    {
        void Dump(IList<T> collection, string path = null);

        IList<T> GetDump();
    }
}
