using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Project_v3.Models
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Films> Films { get; set; }
        public DbSet<Director> Directors { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}
