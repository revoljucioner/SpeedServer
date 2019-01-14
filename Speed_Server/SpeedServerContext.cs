using Microsoft.EntityFrameworkCore;

namespace SpeedServer.Core
{
    public class SpeedServerContext : DbContext
    {
        public SpeedServerContext(DbContextOptions<SpeedServerContext> options)
            : base(options)
        {
        }

        //public DbSet<SpeedModel> SpeedModels { get; set; }

    }
}