using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Project_v3.Models
{
    public class AplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Films> Films { get; set; }
        public DbSet<Director> Directors { get; set; }
        
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>()
                .HasMany(x => x.films)
                .WithMany(x => x.Users);
                

       
        }
    }
}
