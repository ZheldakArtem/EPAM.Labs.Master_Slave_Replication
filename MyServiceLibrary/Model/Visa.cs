using System;

namespace ServiceLibrary.Model
{
    [Serializable]
    public class Visa
    {
        public string Country { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
