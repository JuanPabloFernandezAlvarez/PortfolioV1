
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class PortfolioDbContext : DbContext
    {
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<User> Users { get; set; }

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {
        }
    }

}
