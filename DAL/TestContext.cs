
using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TestContext : IdentityDbContext<User, ApplicationRole, string>
    {
        public TestContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
