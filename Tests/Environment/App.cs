using System.Configuration;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Tests.Helpers;

namespace Tests.Environment
{
    public class App
    {
        public static Configuration Configuration { get; set; }


        static App()
        {
            var config = (Configuration)ConfigurationManager.GetSection("Configuration");
            //
            var ttt = ConfigurationManager.AppSettings;
            var tttc = ttt.Count;
            //
            config.Environment = GetEnvironmentByName(config.EnvironmentName);
            App.Configuration = config;
        }

        private static Environment GetEnvironmentByName(EnvironmentName environmentName)
        {
            var environmentConfigPath = Path.Combine(PathHelper.GetProjectPath(), "env.json");
            var environmentArray = JsonConvert.DeserializeObject<Environment[]>(File.ReadAllText(environmentConfigPath));
            return environmentArray.First(i => i.EnvironmentName.Equals(environmentName));
        }
    }
}
