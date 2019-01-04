using Microsoft.EntityFrameworkCore;

namespace Speed_Server
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