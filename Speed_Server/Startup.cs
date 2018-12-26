using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpeedServerApi.Models;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using Speed_Server.Helpers;

namespace SpeedServerApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            SetAppConfiguration();
            var builder = new ConfigurationBuilder()
                .AddXmlFile("C:\\Users\\Serhii_Mykhailov\\source\\repos\\Speed_Server_november\\Speed_Server\\app.config", optional: false, reloadOnChange: false);
                //.AddJsonFile("env.json", optional: false, reloadOnChange: false);

            Configuration = builder.Build();

            var pathToAppConfig = Path.Combine(PathHelper.GetProjectPath(), "app.config");
            var sr = new StreamReader(pathToAppConfig);
            var appConfig = sr.ReadToEnd();
            //
            var doc = new XmlDocument();
            doc.Load(pathToAppConfig);
            XmlNodeList nodes = doc.GetElementsByTagName("Configuration");
            var node = nodes[0];
            //
            //
            var serializer = new XmlSerializer(typeof(Speed_Server.Configuration));
            var ggg = new Speed_Server.Configuration();
            ggg.EnvironmentName = "sdsdsdsdss";
            //serializer.Serialize(new FileStream("C:\\Users\\Serhii_Mykhailov\\source\\repos\\Speed_Server_november\\Speed_Server\\app.config"), ggg);
            using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, ggg);

                //Console.WriteLine("Объект сериализован");
            }
            //
            var i = (Speed_Server.Configuration)serializer.Deserialize(sr);
        }

        private void SetAppConfiguration()
        {
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SpeedServerContext>(opt => opt.UseInMemoryDatabase("SpeedServerList"));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
            app.UseMvc();
        }
    }
}