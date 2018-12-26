using System;
using Configuration;

namespace Speed_Server
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("Configuration")]
    public class Configuration
    {
        [System.Xml.Serialization.XmlElement("BaseUrl")]
        public string BaseUrl { get; set; }

        [System.Xml.Serialization.XmlElement("EnvironmentName")]
        public string EnvironmentName { get; set; }


        //static Configuration()
        //{
        //    var configProvider = new FileConfiguration(@"app.config").CreateProvider();
        //}
    }
}
