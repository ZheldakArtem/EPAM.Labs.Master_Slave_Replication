using System.Configuration;

namespace ReplicationAPI.CustomSections
{
    public class CustomConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("serviceNodes")]
        public PortsCollection ServiceNodesItems
        {
            get { return ((PortsCollection)(base["serviceNodes"])); }
        }
    }

    [ConfigurationCollection(typeof(ServiceNode))]
    public class PortsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceNode();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceNode)(element)).Port;
        }

        public ServiceNode this[int indx]
        {
            get { return (ServiceNode)BaseGet(indx); }
        }
    }

    public class ServiceNode : ConfigurationElement
    {
        [ConfigurationProperty("nodeType", DefaultValue = "Slave", IsKey = false, IsRequired = true)]
        public string NodeType
        {
            get { return ((string)(base["nodeType"])); }
            set { base["nodeType"] = value; }
        }

        [ConfigurationProperty("port", DefaultValue = 0, IsKey = true, IsRequired = true)]
        public int Port
        {
            get { return ((int)(base["port"])); }
            set { base["port"] = value; }
        }
    }

    /// <summary>
    /// Class which convert PortsCollection to int[]
    /// </summary>
    public static class Converter
    {
        public static int[] ToArray(this PortsCollection ports)
        {
            var resultArr = new int[ports.Count];
            for (int i = 0; i < ports.Count; i++)
            {
                resultArr[i] = ports[i].Port;
            }

            return resultArr;
        }
    }
}
