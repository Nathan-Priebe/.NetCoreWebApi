using Microsoft.EntityFrameworkCore;

namespace NetCoreWebAPI.Entities
{
    public sealed class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options): base(options)
        {
            Database.Migrate();
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
    }
}
