using System;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace Speed_Server
{
    public abstract class XmlDeserializeConfigSectionHandler : IConfigurationSectionHandler
    {
        protected XmlDeserializeConfigSectionHandler()
            : base()
        {
        }

        #region IConfigurationSectionHandler Members

        /// <summary>
        /// A method which is called by the CLR when parsing the App.Config file. If custom sections
        /// are found, then an entry in the configuration file will tell the runtime to call this method,
        /// passing in the XmlNode required.
        /// </summary>
        /// <param name="parent">The configuration settings in a corresponding parent configuration section. Passed in via the CLR</param>
        /// <param name="configContext">An <see cref="HttpConfigurationContext"/> when Create is called from the ASP.NET configuration system. Otherwise, 
        /// this parameter is reserved and is a null reference (Nothing in Visual Basic). Passed in via the CLR</param>
        /// <param name="section">The <see cref="XmlNode"/> that contains the configuration information from the configuration file. 
        /// Provides direct access to the XML contents of the configuration section. 	Passed in via the CLR.</param>
        /// <returns>The Deserialized object as an object</returns>
        /// <exception cref="ConfigurationException">The Configuration file is not well formed,
        /// or the Custom section is not configured correctly, or the type of configuration handler was not specified correctly
        /// or the type of object was not specified correctly.
        /// or the copn</exception>
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            Type t = this.GetType();
            XmlSerializer ser = new XmlSerializer(t);
            XmlNodeReader xNodeReader = new XmlNodeReader(section);
            return ser.Deserialize(xNodeReader);
        }
        #endregion
    }
}
