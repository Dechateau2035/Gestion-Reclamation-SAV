using API_Backend.Models;

namespace API_Backend.Repo.IRepositories
{
    public interface IUtilisateurRepository
    {
        Task<IEnumerable<Utilisateur>> GetUtilisateurs(TypeUser? typeUser);
        Task<Utilisateur> GetUtilisateur(int userId);
        Task<Utilisateur> AddUtilisateur(Utilisateur utilisateur);
        //Task<Utilisateur> UpdateUtilisateur(Utilisateur utilisateur);
        Task<Utilisateur> UpdateUtilisateur(int id, Utilisateur utilisateur);
        Task<Utilisateur> DeleteUtilisateur(int userId);
        Task<IEnumerable<Utilisateur>> SearchUtilisateur(string character, TypeUser? typeUser);
    }
}
