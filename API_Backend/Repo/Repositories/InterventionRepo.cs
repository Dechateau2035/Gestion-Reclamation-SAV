using API_Backend.Context;
using API_Backend.Models;
using API_Backend.Repo.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API_Backend.Repo.Repositories
{
    public class InterventionRepo : IInterventionRepo
    {
        private readonly AppDbContext appDbContext;
        public InterventionRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Intervention> AddIntervention(Intervention intervention)
        {
            var reclamation = await appDbContext.Reclamations
                .Include(r => r.Article)
                .FirstOrDefaultAsync(r => r.ReclamationId == intervention.ReclamationId);
            if (reclamation == null || reclamation.Article == null)
            {
                throw new InvalidOperationException("Réclamation ou article introuvable.");
            }

            if ((bool)reclamation.Article.SousGarantie)
            {
                intervention.EstGratuite = true;
                intervention.CoutManuel = 0;
            }
            else intervention.EstGratuite = false;

            var result = await appDbContext.Interventions.AddAsync(intervention);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Intervention> DeleteIntervention(int id)
        {
            var result = await appDbContext.Interventions
                .FirstOrDefaultAsync(i => i.InterventionId == id);
            if (result != null)
            {
                appDbContext.Interventions.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Intervention> GetIntervention(int Id)
        {
            return await appDbContext.Interventions
                .Include(i => i.Reclamation)
                .Include(i => i.Utilisateur)
                .Include(r => r.PieceUtilises)
                .FirstOrDefaultAsync(i => i.InterventionId == Id);
        }

        public async Task<IEnumerable<Intervention>> GetInterventions()
        {
            return await appDbContext.Interventions
                .Include(r => r.Reclamation)
                .Include(r => r.Utilisateur)
                .Include(r => r.PieceUtilises)
                .ToListAsync();
        }

        public async Task<IEnumerable<Intervention>> GetInterventionsByUser(int userId)
        {
            return await appDbContext.Interventions
                .Include(r => r.Reclamation)
                .Where(r => r.UtilisateurId == userId)
                .ToListAsync();
        }

        public async Task<Intervention> UpdateIntervention(int id, Intervention intervention)
        {
            var existingIntervention = await appDbContext.Interventions.FindAsync(id);
            if (existingIntervention!= null)
            {
                var reclamation = await appDbContext.Reclamations
                .Include(r => r.Article)
                .FirstOrDefaultAsync(r => r.ReclamationId == intervention.ReclamationId);
                if (reclamation == null || reclamation.Article == null)
                {
                    throw new InvalidOperationException("Réclamation ou article introuvable.");
                }

                if ((bool)reclamation.Article.SousGarantie)
                {
                    intervention.EstGratuite = true;
                    intervention.CoutManuel = 0;
                }
                else intervention.EstGratuite = false;

                existingIntervention.DateIntervention = intervention.DateIntervention ?? existingIntervention.DateIntervention;
                existingIntervention.CoutManuel = intervention.CoutManuel ?? existingIntervention.CoutManuel;
                existingIntervention.Statut = intervention.Statut ?? existingIntervention.Statut;

                await appDbContext.SaveChangesAsync();
                return intervention;
            }
            return null;
        }
    }
}
