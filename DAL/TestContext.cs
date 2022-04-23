
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
