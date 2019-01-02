using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Tests.Helpers;

namespace Tests.Environment
{
    public class App
    {
        private static Configuration _configuration { get; set; }
        public static string WebConfigPath = Path.Combine(PathHelper.GetProjectPath(), "web.config");
        public static string EnvConfigPath = Path.Combine(PathHelper.GetProjectPath(), "env.json");

        public static Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = GetConfigurationFromConfigFile();
                    _configuration.Environment = GetEnvironmentFromConfigFileByName(_configuration.EnvironmentName);
                }
                return _configuration;
            }
        }

        private static Configuration GetConfigurationFromConfigFile()
        {
            var stream = File.Open(WebConfigPath, FileMode.Open);
            var ser = new XmlSerializer(typeof(Configuration));
            return ser.Deserialize(stream) as Configuration;
        }

        private static Environment GetEnvironmentFromConfigFileByName(EnvironmentName environmentName)
        {
            var environmentArray = JsonConvert.DeserializeObject<Environment[]>(File.ReadAllText(EnvConfigPath));
            return environmentArray.First(i => i.EnvironmentName.Equals(environmentName));
        }
    }
}
