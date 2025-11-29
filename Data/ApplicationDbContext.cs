using Fudbalski_turnir.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fudbalski_turnir.Data
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
                .UsingEntity(j => j.ToTable("Igra")); // EF će automatski kreirati join tabelu

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Fudbalski_turnir.Models.Klub> Klub { get; set; } = default!;
        public DbSet<Fudbalski_turnir.Models.Igrac> Igrac { get; set; } = default!;
        public DbSet<Fudbalski_turnir.Models.Menadzer> Menadzer { get; set; } = default!;
        public DbSet<Fudbalski_turnir.Models.Osoba> Osoba { get; set; } = default!;
        public DbSet<Fudbalski_turnir.Models.Sponzor> Sponzor { get; set; } = default!;
        public DbSet<Fudbalski_turnir.Models.Turnir> Turnir { get; set; } = default!;
        public DbSet<Fudbalski_turnir.Models.Utakmica> Utakmica { get; set; } = default!;
    }

}
