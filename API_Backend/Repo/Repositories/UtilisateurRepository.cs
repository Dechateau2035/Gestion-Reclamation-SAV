using API_Backend.Context;
using API_Backend.Models;
using API_Backend.Repo.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API_Backend.Repo.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly AppDbContext appDbContext;
        public UtilisateurRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Utilisateur> AddUtilisateur(Utilisateur utilisateur)
        {
            var result = await appDbContext.Utilisateurs.AddAsync(utilisateur);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Utilisateur> DeleteUtilisateur(int userId)
        {
            var result = await appDbContext.Utilisateurs
                .FirstOrDefaultAsync(u => u.UtilisateurId == userId);
            if (result != null)
            {
                appDbContext.Utilisateurs.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Utilisateur>> GetUtilisateurs(TypeUser? typeUser)
        {
            IQueryable<Utilisateur> query = appDbContext.Utilisateurs;
            if(typeUser != null)
                query = query.Where(u => u.Type_Utilisateur == typeUser.ToString());
            return await query.ToListAsync();
        }

        public async Task<Utilisateur> GetUtilisateur(int userId)
        {
            return await appDbContext.Utilisateurs
                .FirstOrDefaultAsync(u => u.UtilisateurId == userId);
        }
        public async Task<IEnumerable<Utilisateur>> SearchUtilisateur(string character, TypeUser? typeUser)
        {
            IQueryable<Utilisateur> query = appDbContext.Utilisateurs;
            if (!string.IsNullOrEmpty(character))
                query = query.Where(u => u.Nom.Contains(character) || u.Email.Contains(character));
            if (typeUser != null)
                query = query.Where(u => u.Type_Utilisateur == typeUser.ToString());
            return await query.ToListAsync();
        }

        public async Task<Utilisateur> UpdateUtilisateur(int id,Utilisateur utilisateur)
        {
            var existingUser = await appDbContext.Utilisateurs.FindAsync(id);
            if(existingUser != null)
            {
                existingUser.Nom = utilisateur.Nom;
                existingUser.Email = utilisateur.Email;
                existingUser.Adresse = utilisateur.Adresse;
                existingUser.Telephone = utilisateur.Telephone;
                existingUser.Type_Utilisateur = utilisateur .Type_Utilisateur;

                await appDbContext.SaveChangesAsync();
                return utilisateur;
            }
            return null;
        }
    }
}
