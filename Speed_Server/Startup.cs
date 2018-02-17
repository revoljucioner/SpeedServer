using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpeedServerApi.Models;
using System;
using System.Globalization;
using System.Threading;

namespace SpeedServerApi
{
    public class Startup
    {
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