using Microsoft.EntityFrameworkCore;
using TableWebApp.Models;

namespace TableWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options):base(options) { }

        public DbSet<CSVData> CSVData { get; set; }


    }
}
