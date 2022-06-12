
using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    //public class TestContext : IdentityDbContext<User, ApplicationRole, string>
    //{
    //    public TestContext(DbContextOptions options) : base(options)
    //    {
    //    }
    //    public DbSet<ApplicationRole> Roles { get; set; }
    //    public DbSet<User> Users { get; set; }
    //    public DbSet<Product> Products { get; set; }
    //}
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions items) : base(items)
        {

        }
        public DbSet<ApplicationRole> AppRoles { get; set; }
        public DbSet<User> AppUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppFile> AppFiles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandFile> BrandFiles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionFile> PromotionFiles { get; set; }


    }
}
