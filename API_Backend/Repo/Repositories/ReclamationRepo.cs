using API_Backend.Context;
using API_Backend.Models;
using API_Backend.Repo.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace API_Backend.Repo.Repositories
{
    public class ReclamationRepo : IReclamationRepo
    {
        private readonly AppDbContext appDbContext;
        public ReclamationRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Reclamation> AddReclamation(Reclamation reclamation)
        {
            var result = await appDbContext.Reclamations.AddAsync(reclamation);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Reclamation> DeleteReclamation(int id)
        {
            var result = await appDbContext.Reclamations
                .FirstOrDefaultAsync(r => r.ReclamationId == id);
            if (result != null)
            {
                appDbContext.Reclamations.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Reclamation> GetReclamation(int Id)
        {
            return await appDbContext.Reclamations
                .Include(r => r.Article)
                .Include(r => r.Utilisateur)
                .FirstOrDefaultAsync(r => r.ReclamationId == Id);
        }

        public async Task<IEnumerable<Reclamation>> GetReclamations()
        {
            return await appDbContext.Reclamations
                .Include(r => r.Article)
                .Include(r => r.Interventions)
                .Include(r => r.Utilisateur)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Reclamation>> GetReclamationsByUser(int userId)
        {
            return await appDbContext.Reclamations
                .Include(r => r.Article)
                .Include(r => r.Interventions)
                .AsNoTracking()
                .Where(r => r.UtilisateurId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reclamation>> SearchReclamation(string character)
        {
            IQueryable<Reclamation> query = appDbContext.Reclamations
                .Include(r => r.Article)
                .Include(r => r.Utilisateur)
                .Include(r => r.Interventions)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(character))
                query = query.Where(r => r.Description.Contains(character) || r.Article.Nom.Contains(character));
            return await query.ToListAsync();
        }

        public async Task<Reclamation> UpdateReclamation(int id, Reclamation reclamation)
        {
            var existingReclamation = await appDbContext.Reclamations.FindAsync(id);
            if (existingReclamation != null)
            {
                existingReclamation.DateSoumission = reclamation.DateSoumission ?? existingReclamation.DateSoumission;
                existingReclamation.Description = reclamation.Description ?? existingReclamation.Description;
                existingReclamation.Etat_Reclamation = reclamation.Etat_Reclamation ?? existingReclamation.Etat_Reclamation;

                await appDbContext.SaveChangesAsync();
                return reclamation;
            }
            return null;
        }
    }
}
