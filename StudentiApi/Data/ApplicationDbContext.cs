using Microsoft.EntityFrameworkCore;
using StudentiApi.Models;

namespace EcommerceApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Student> Students { get; set; }

    }
}