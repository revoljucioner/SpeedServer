using System.Xml.Serialization;

namespace Tests.Environment
{
    [XmlRoot("Configuration")]
    public class Configuration
    {
        [XmlElement("EnvironmentName")]
        public EnvironmentName EnvironmentName { get; set; }

        public Environment Environment { get; set; }
    }
}
