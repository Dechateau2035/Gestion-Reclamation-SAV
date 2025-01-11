using API_Backend.Models;

namespace API_Backend.Repo.IRepositories
{
    public interface IReclamationRepo
    {
        Task<IEnumerable<Reclamation>> GetReclamations();
        Task<IEnumerable<Reclamation>> GetReclamationsByUser(int userId);
        Task<Reclamation> GetReclamation(int Id);
        Task<Reclamation> AddReclamation(Reclamation reclamation);
        Task<Reclamation> UpdateReclamation(int id, Reclamation reclamation);
        Task<Reclamation> DeleteReclamation(int id);
        Task<IEnumerable<Reclamation>> SearchReclamation(string character);
    }
}
