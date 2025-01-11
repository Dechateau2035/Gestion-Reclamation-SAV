using API_Backend.Models;
using API_Backend.Repo.IRepositories;
using API_Backend.Repo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Backend.Controllers
{
    [Route("api/piecesChange")]
    [ApiController]
    public class PieceRechangeController : ControllerBase
    {
        private readonly IPieceRechangeRepo pieceRechangeRepo;
        public PieceRechangeController(IPieceRechangeRepo pieceRechangeRepo)
        {
            this.pieceRechangeRepo = pieceRechangeRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetPRs()
        {
            try
            {
                return Ok(await pieceRechangeRepo.GetPieceRechanges());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PieceRechange>> GetPR(int id)
        {
            try
            {
                var result = await pieceRechangeRepo.GetPieceRechange(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PieceRechange>> AddPR([FromForm] PieceRechange piece)
        {
            try
            {
                if (piece == null)
                    return BadRequest();
                var addPR = await pieceRechangeRepo.AddPieceRechange(piece);

                return CreatedAtAction(nameof(GetPR), new { id = addPR.PieceRechangeId }, addPR);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de l'ajout de la nouvelle pièce : {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PieceRechange>>> SearchPR([FromQuery] String character)
        {
            try
            {
                var result = await pieceRechangeRepo.SearchPieceRechange(character);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PieceRechange>> DeletePR(int id)
        {
            try
            {
                var piece = await pieceRechangeRepo.GetPieceRechange(id);
                if (piece == null)
                    return NotFound($"Pièce with Id:{id} not found");
                return Ok(await pieceRechangeRepo.DeletePieceRechange(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la supression de données : {ex.Message}");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<PieceRechange>> UpdatePR(int id, PieceRechange piece)
        {
            try
            {
                if (id == null)
                    return BadRequest("Pièce ID mismatch");

                var pieceToUpdate = await pieceRechangeRepo.GetPieceRechange(id);
                if (pieceToUpdate == null)
                    return NotFound($"Pièce with Id:{id} not found");

                await pieceRechangeRepo.UpdatePieceRechange(id, piece);

                return Ok(pieceToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la mise à jour de données : {ex.Message}");
            }
        }
    }
}
