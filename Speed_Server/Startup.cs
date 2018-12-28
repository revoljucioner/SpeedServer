using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SpeedServerApi.Models;
using Speed_Server.Helpers;

namespace Speed_Server
{
    public class Startup
    {
        public Startup()
        {
            var config = (Configuration)ConfigurationManager.GetSection("Configuration");
            config.Environment = GetEnvironmentByName(config.EnvironmentName);
            App.Configuration = config;
        }

        private Environment GetEnvironmentByName(EnvironmentName environmentName)
        {
            var environmentConfigPath = Path.Combine(PathHelper.GetProjectPath(), "env.json");
            var environmentArray = JsonConvert.DeserializeObject<Environment[]>(File.ReadAllText(environmentConfigPath));
            return environmentArray.First(i=>i.EnvironmentName.Equals(environmentName));
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