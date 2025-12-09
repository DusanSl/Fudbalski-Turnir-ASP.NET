using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FudbalskiTurnir.DAL.Models;


namespace FudbalskiTurnir.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klub>()
                .HasMany(k => k.Utakmice)
                .WithMany(u => u.Klubovi)
                .UsingEntity(j => j.ToTable("Igra"));

            modelBuilder.Entity<Turnir>()
                .HasMany(t => t.Sponzori)
                .WithMany(s => s.Turniri)
                .UsingEntity(j => j.ToTable("Sponzorise"));

            modelBuilder.Entity<Turnir>()
                .HasMany(t => t.Klubovi)
                .WithMany(k => k.Turniri)
                .UsingEntity(j => j.ToTable("Ucestvuje"));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Igrac> Igrac { get; set; }
        public DbSet<Klub> Klub { get; set; }
        public DbSet<Menadzer> Menadzer { get; set; }
        public DbSet<Osoba> Osoba { get; set; }
        public DbSet<Sponzor> Sponzor { get; set; }
        public DbSet<Turnir> Turnir { get; set; }
        public DbSet<Utakmica> Utakmica { get; set; }
    }
}
