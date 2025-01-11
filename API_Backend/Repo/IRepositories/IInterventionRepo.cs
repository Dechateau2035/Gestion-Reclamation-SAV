using API_Backend.Models;

namespace API_Backend.Repo.IRepositories
{
    public interface IInterventionRepo
    {
        Task<IEnumerable<Intervention>> GetInterventions();
        Task<IEnumerable<Intervention>> GetInterventionsByUser(int userId);
        Task<Intervention> GetIntervention(int Id);
        Task<Intervention> AddIntervention(Intervention intervention);
        Task<Intervention> UpdateIntervention(int id, Intervention intervention);
        Task<Intervention> DeleteIntervention(int id);
    }
}
