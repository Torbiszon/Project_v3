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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Films>().HasData(
                new Films() { FilmId = 1, FilmName = "nwm", FilmDescription = "opis", FilmType = "action", FilmCount=8 },
                new Films() { FilmId = 2, FilmName = "nwm1", FilmDescription = "opis1", FilmType = "action", FilmCount = 8 },
                new Films() { FilmId = 3, FilmName = "nwm2", FilmDescription = "opis2", FilmType = "action", FilmCount = 8 },
                new Films() { FilmId = 4, FilmName = "nwm3", FilmDescription = "opis3", FilmType = "action", FilmCount = 8 }
            );
            modelBuilder.Entity<Director>().HasData(
                new Director() { Id = 1, Name = "kamil", Surname ="nowak" },
                new Director() { Id = 2, Name = "anna", Surname = "kowalski" },
                new Director() { Id = 3, Name = "milosz", Surname = "marciniak" }
            );
       
        }
    }
}
