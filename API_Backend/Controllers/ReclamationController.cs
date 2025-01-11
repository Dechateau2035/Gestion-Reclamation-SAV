using API_Backend.Models;
using API_Backend.Models.Dtos;
using API_Backend.Repo.IRepositories;
using API_Backend.Repo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_Backend.Controllers
{
    [Route("api/reclamations/")]
    [ApiController]
    public class ReclamationController : ControllerBase
    {
        private readonly IReclamationRepo reclamationRepo;
        private readonly IUtilisateurRepository utilisateurRepository;
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;
        public ReclamationController(IArticleRepository articleRepository, IMapper mapper, IUtilisateurRepository utilisateurRepository, IReclamationRepo reclamationRepo)
        {
            this.reclamationRepo = reclamationRepo;
            this.mapper = mapper;
            this.utilisateurRepository = utilisateurRepository;
            this.articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetReclamations()
        {
            try
            {
                var reclamtions = await reclamationRepo.GetReclamations();
                var data = mapper.Map<IEnumerable<ReclamationDetailDto>>(reclamtions);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReclamation(int id)
        {
            try
            {
                var result = await reclamationRepo.GetReclamation(id);
                if (result == null) return NotFound();
                return Ok(mapper.Map<ReclamationDetailDto>(result));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReclamtion([FromForm] ReclamationDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest();

                var user = await utilisateurRepository.GetUtilisateur(dto.UtilisateurId);
                if (user == null) return BadRequest("Ivalid user Id");
                if (user.Type_Utilisateur != TypeUser.Client.ToString()) return BadRequest("L'utilisateur qui soumet une reclamation doit etre un client");

                var article = await articleRepository.GetArticle(dto.ArticleId);
                if (article == null) return BadRequest("Ivalid article Id");

                var reclamation = mapper.Map<Reclamation>(dto);
                await reclamationRepo.AddReclamation(reclamation);

                return Ok(reclamation);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de l'ajout d'une nouvelle reclamation : {ex.Message}");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateReclamation(int id, [FromForm] ReclamationDto dto)
        {
            try
            {
                if (id == null)
                    return BadRequest("Reclamation ID mismatch");

                var reclamatoinToUpdate = await reclamationRepo.GetReclamation(id);
                if (reclamatoinToUpdate == null)
                    return NotFound($"Reclamation with Id:{id} not found");

                var user = await utilisateurRepository.GetUtilisateur(dto.UtilisateurId);
                if (user == null) return BadRequest("Ivalid user Id");

                var article = await articleRepository.GetArticle(dto.ArticleId);
                if (article == null) return BadRequest("Ivalid article Id");

                reclamatoinToUpdate.Description = dto.Description;
                reclamatoinToUpdate.DateSoumission = dto.DateSoumission;
                reclamatoinToUpdate.Etat_Reclamation = dto.Etat_Reclamation;
                reclamatoinToUpdate.UtilisateurId = dto.UtilisateurId;
                reclamatoinToUpdate.ArticleId = dto.ArticleId;

                await reclamationRepo.UpdateReclamation(id, reclamatoinToUpdate);

                return Ok(reclamatoinToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la mise à jour de données : {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReclamation(int id)
        {
            try
            {
                var reclamation = await reclamationRepo.GetReclamation(id);
                if (reclamation == null)
                    return NotFound($"Reclamation with Id:{id} not found");
                return Ok(await reclamationRepo.DeleteReclamation(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la supression de données : {ex.Message}");
            }
        }
    }
}
