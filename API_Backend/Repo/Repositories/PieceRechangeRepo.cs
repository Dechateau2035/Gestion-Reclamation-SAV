using API_Backend.Context;
using API_Backend.Models;
using API_Backend.Repo.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API_Backend.Repo.Repositories
{
    public class PieceRechangeRepo : IPieceRechangeRepo
    {
        private readonly AppDbContext appDbContext;
        public PieceRechangeRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<PieceRechange> AddPieceRechange(PieceRechange pieceRechange)
        {
            var result = await appDbContext.PieceRechanges.AddAsync(pieceRechange);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PieceRechange> DeletePieceRechange(int id)
        {
            var result = await appDbContext.PieceRechanges
                .FirstOrDefaultAsync(pr => pr.PieceRechangeId == id);
            if (result != null)
            {
                appDbContext.PieceRechanges.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<PieceRechange>> GetPieceRechanges()
        {
            return await appDbContext.PieceRechanges.ToListAsync();
        }

        public async Task<PieceRechange> GetPieceRechange(int Id)
        {
            return await appDbContext.PieceRechanges
                .FirstOrDefaultAsync(pr => pr.PieceRechangeId == Id);
        }

        public async Task<IEnumerable<PieceRechange>> SearchPieceRechange(string character)
        {
            IQueryable<PieceRechange> query = appDbContext.PieceRechanges;
            if (!string.IsNullOrEmpty(character))
                query = query.Where(a => a.Nom.Contains(character));
            return await query.ToListAsync();
        }

        public async Task<PieceRechange> UpdatePieceRechange(int id, PieceRechange pieceRechange)
        {
            var existingPR = await appDbContext.PieceRechanges.FindAsync(id);
            if (existingPR != null)
            {
                existingPR.Nom = pieceRechange.Nom ?? existingPR.Nom;
                existingPR.Prix = pieceRechange.Prix ?? existingPR.Prix;
                existingPR.StockDisponible = pieceRechange.StockDisponible ?? existingPR.StockDisponible;

                await appDbContext.SaveChangesAsync();
                return pieceRechange;
            }
            return null;
        }
    }
}
