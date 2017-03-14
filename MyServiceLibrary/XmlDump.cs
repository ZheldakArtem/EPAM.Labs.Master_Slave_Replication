using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ServiceLibrary.Model;

namespace ServiceLibrary
{
    public class XmlDump : IDumper<User>
    {
        private string filePath;

        public XmlDump()
        {
            this.filePath = ConfigurationManager.AppSettings.Get("FilePath");
        }

        public IList<User> GetDump()
        {
            var formatter = new XmlSerializer(typeof(List<User>));
            IList<User> users;

            using (FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate))
            {
                users = (IList<User>)formatter.Deserialize(fs);
            }

            return users;
        }

        public void Dump(IList<User> users, string path = null)
        {
            path = path ?? this.filePath;
            var formatter = new XmlSerializer(typeof(List<User>));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, users);
            }
        }
    }
}