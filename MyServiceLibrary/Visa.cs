using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary
{
    [Serializable]
    public class Visa
    {
        public string Country { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
