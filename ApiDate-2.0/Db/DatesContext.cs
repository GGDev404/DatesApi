using Microsoft.EntityFrameworkCore;
using ApiDate_2._0.Models;

namespace ApiDate_2._0.Db
{
    public class DatesContext : DbContext
    {
        public DatesContext(DbContextOptions<DatesContext> options) : base(options)
        {
        }
        public DbSet<Days> Days { get; set; }
    }
}