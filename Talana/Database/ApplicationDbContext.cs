using Microsoft.EntityFrameworkCore;
using Talana.Database.Models;

namespace Talana.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Holiday> Holidays { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database/app.db");
        }
    }
}
