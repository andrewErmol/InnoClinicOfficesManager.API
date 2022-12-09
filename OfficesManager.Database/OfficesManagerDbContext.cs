using Microsoft.EntityFrameworkCore;
using OfficesManager.Database.EntityForRepository;

namespace OfficesManager.Database
{
    public class OfficesManagerDbContext : DbContext
    {
        public OfficesManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OfficeEntity> Offices { get; set; }
    }
}
