using BusinessLogicLayer;
using BusinessLogicLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CatsContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }
        public CatsContext(DbContextOptions<CatsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
