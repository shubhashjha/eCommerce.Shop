using eCommerce.CouponService.Model;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.CouponService.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {

        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon 
            {
                Code = "COU01",
                DiscountAmount = 5,
                Id = 1,
                MinAmount = 10
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Code = "COU02",
                DiscountAmount = 15,
                Id = 2,
                MinAmount = 15
            });
        }
    }
}
