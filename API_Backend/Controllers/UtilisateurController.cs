using API_Backend.Models;
using API_Backend.Models.Dtos;
using API_Backend.Repo.IRepositories;
using API_Backend.Repo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Backend.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateurRepository utilisateurRepository;
        private readonly IReclamationRepo reclamationRepo;
        private readonly IMapper mapper;
        public UtilisateurController(IReclamationRepo reclamationRepo, IMapper mapper, IUtilisateurRepository utilisateurRepository)
        {
            this.utilisateurRepository = utilisateurRepository;
            this.mapper = mapper;
            this.reclamationRepo = reclamationRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers(TypeUser? typeUser)
        {
            try
            {
                return Ok(await utilisateurRepository.GetUtilisateurs(typeUser));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpGet("reclamations")]
        public async Task<IActionResult> GetReclamations(int id)
        {
            try
            {
                if (id == null) return BadRequest("Reclamation Id is mismatch");

                var reclamtions = await reclamationRepo.GetReclamationsByUser(id);
                var data = mapper.Map<IEnumerable<ReclamationDetailDto>>(reclamtions);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpGet("searchReclamation")]
        public async Task<IActionResult> SearchReclamation(string character)
        {
            try
            {
                var reclamtions = await reclamationRepo.SearchReclamation(character);
                var data = mapper.Map<IEnumerable<ReclamationDetailDto>>(reclamtions);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Utilisateur>> GetUser(int id)
        {
            try
            {
                var result = await utilisateurRepository.GetUtilisateur(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Utilisateur>> CreateUser([FromForm] Utilisateur user)
        {
            try
            {
                if (user == null)
                    return BadRequest();
                var createdUser = await utilisateurRepository.AddUtilisateur(user);

                return CreatedAtAction(nameof(GetUser), new { id = createdUser.UtilisateurId }, createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la creation du nouvel utilisateur : {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> SearchUser([FromQuery] String name, [FromQuery] TypeUser? typeUser)
        {
            try
            {
                var result = await utilisateurRepository.SearchUtilisateur(name, typeUser);
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
        public async Task<ActionResult<Utilisateur>> DeleteUser(int id)
        {
            try
            {
                var user = await utilisateurRepository.GetUtilisateur(id);
                if (user == null)
                    return NotFound($"user with Id:{id} not found");
                return Ok(await utilisateurRepository.DeleteUtilisateur(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la supression de données : {ex.Message}");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Utilisateur>> UpdateUser(int id, Utilisateur user)
        {
            try
            {
                if (id == null)
                    return BadRequest("User ID mismatch");

                var userToUpdate = await utilisateurRepository.GetUtilisateur(id);
                if (userToUpdate == null)
                    return NotFound($"user with Id:{id} not found");

                await utilisateurRepository.UpdateUtilisateur(id, user);

                return Ok(userToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la mise à jour de données : {ex.Message}");
            }
        }
    }
}
