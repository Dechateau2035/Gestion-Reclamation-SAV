using API_Backend.Models;

namespace API_Backend.Repo.IRepositories
{
    public interface IPieceRechangeRepo
    {
        Task<IEnumerable<PieceRechange>> GetPieceRechanges();
        Task<PieceRechange> GetPieceRechange(int Id);
        Task<PieceRechange> AddPieceRechange(PieceRechange pieceRechange);
        Task<PieceRechange> UpdatePieceRechange(int id, PieceRechange pieceRechange);
        Task<PieceRechange> DeletePieceRechange(int id);
        Task<IEnumerable<PieceRechange>> SearchPieceRechange(string character);
    }
}
