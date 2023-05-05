using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Data
{
    public class FullStackDbContext : DbContext
    {
        public FullStackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Breastfeeding> Breastfeedings { get; set; }

        public DbSet<BowelMovement> BowelMovements { get; set; }

        public DbSet<Sleep> Sleeps { get; set; }

        public DbSet<Calendar> Calendars { get; set; }
    }
}
