using API_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Backend.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Reclamation> Reclamations { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<PieceRechange> PieceRechanges { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<PieceUtilise> PieceUtilises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relation Intervention -> Reclamation
            modelBuilder.Entity<Intervention>()
                .HasOne(i => i.Reclamation)
                .WithMany(r => r.Interventions)
                .HasForeignKey(i => i.ReclamationId)
                .OnDelete(DeleteBehavior.Cascade); // Conserve la cascade ici

            // Relation Intervention -> Utilisateur
            modelBuilder.Entity<Intervention>()
                .HasOne(i => i.Utilisateur)
                .WithMany()
                .HasForeignKey(i => i.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade ici

            // Relation Intervention -> PieceUtilisee
            modelBuilder.Entity<PieceUtilise>()
                .HasKey(pu => pu.PieceUtiliseId); // Clé primaire

            modelBuilder.Entity<PieceUtilise>()
                .HasOne(pu => pu.Intervention)
                .WithMany(i => i.PieceUtilises)
                .HasForeignKey(pu => pu.InterventionId);

            modelBuilder.Entity<PieceUtilise>()
                .HasOne(pu => pu.PieceRechange)
                .WithMany(pr => pr.PiecesUtilises)
                .HasForeignKey(pu => pu.PieceRechangeId);

            // Relation Reclamation -> Utilisateur
            modelBuilder.Entity<Reclamation>()
                .HasOne(r => r.Utilisateur)
                .WithMany()
                .HasForeignKey(r => r.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade ici

            // Relation Reclamation -> Article
            modelBuilder.Entity<Reclamation>()
                .HasOne(r => r.Article)
                .WithMany()
                .HasForeignKey(r => r.ArticleId)
                .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade ici

            modelBuilder.Entity<Utilisateur>()
                .Property(u => u.Type_Utilisateur)
                .HasDefaultValue(TypeUser.Client.ToString());

            modelBuilder.Entity<Reclamation>()
                .Property(r => r.Etat_Reclamation)
                .HasDefaultValue(EtatReclamation.En_attente_information.ToString());

            modelBuilder.Entity<Facture>()
                .Property(f => f.Statut)
                .HasDefaultValue(StatutFacture.Non_payée.ToString());

            modelBuilder.Entity<Intervention>()
                .Property(i => i.Statut)
                .HasDefaultValue(StatutIntervention.En_attente.ToString());

            base.OnModelCreating(modelBuilder);
        }
    }
}
