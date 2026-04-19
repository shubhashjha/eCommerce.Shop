using eCommerce.RewardService.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.RewardService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Rewards> Rewards { get; set; }

      
    }
}

