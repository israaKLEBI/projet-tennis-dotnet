using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TennisShopApp.Data.Models;

namespace TennisShopApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Joueur> Joueurs { get; set; }
        public DbSet<Tournoi> Tournois { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Gain> Gains { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Tournoi: Unique index on NomT and Ville
            builder.Entity<Tournoi>()
                .HasIndex(t => new { t.NomT, t.Ville })
                .IsUnique();

            // Joueur: Primary key
            builder.Entity<Joueur>()
                .HasKey(j => j.CodeJ);

            // Tournoi: Primary key
            builder.Entity<Tournoi>()
                .HasKey(t => t.CodeT);

            // Phase: Primary key
            builder.Entity<Phase>()
                .HasKey(p => p.CodePh);

            // Gain: Composite key and relationships
            builder.Entity<Gain>()
                .HasKey(g => new { g.CodeT, g.CodeJ, g.CodePh });

            builder.Entity<Gain>()
                .HasOne(g => g.Tournoi)
                .WithMany(t => t.Gains)
                .HasForeignKey(g => g.CodeT)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Gain>()
                .HasOne(g => g.Joueur)
                .WithMany(j => j.Gains)
                .HasForeignKey(g => g.CodeJ)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Gain>()
                .HasOne(g => g.Phase)
                .WithMany(p => p.Gains)
                .HasForeignKey(g => g.CodePh)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}