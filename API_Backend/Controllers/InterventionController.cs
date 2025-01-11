using API_Backend.Models.Dtos;
using API_Backend.Models;
using API_Backend.Repo.IRepositories;
using API_Backend.Repo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Backend.Controllers
{
    [Route("api/interventions/")]
    [ApiController]
    public class InterventionController : ControllerBase
    {
        private readonly IReclamationRepo reclamationRepo;
        private readonly IUtilisateurRepository utilisateurRepository;
        private readonly IInterventionRepo interventionRepo;
        private readonly IMapper mapper;
        public InterventionController(IInterventionRepo interventionRepo, IMapper mapper, IUtilisateurRepository utilisateurRepository, IReclamationRepo reclamationRepo)
        {
            this.reclamationRepo = reclamationRepo;
            this.mapper = mapper;
            this.utilisateurRepository = utilisateurRepository;
            this.interventionRepo = interventionRepo;
        }

        [HttpPost]
        public async Task<IActionResult> AddIntervention([FromForm] InterventionDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest();

                var user = await utilisateurRepository.GetUtilisateur(dto.UtilisateurId);
                if (user == null) return BadRequest("Ivalid user Id");
                if (user.Type_Utilisateur == TypeUser.Client.ToString()) return BadRequest("L'utilisateur qui soumet une reclamation ne peut pas etre un client");

                var reclamation = await reclamationRepo.GetReclamation(dto.ReclamationId);
                if (reclamation == null) return BadRequest("Ivalid reclamation Id");

                var intervention = mapper.Map<Intervention>(dto);
                await interventionRepo.AddIntervention(intervention);

                return Ok(intervention);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de l'ajout d'une nouvelle intervention : {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetIntervention(int id)
        {
            try
            {
                var result = await interventionRepo.GetIntervention(id);
                if (result == null) return NotFound();
                return Ok(mapper.Map<InterventionDetailDto>(result));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInterventions()
        {
            try
            {
                var interventions = await interventionRepo.GetInterventions();
                var data = mapper.Map<IEnumerable<InterventionDetailDto>>(interventions);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la recupération de données : {ex.Message}");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateIntervention(int id, [FromForm] InterventionDto dto)
        {
            try
            {
                if (id == null)
                    return BadRequest("Intervention ID mismatch");

                var interventionToUpdate = await interventionRepo.GetIntervention(id);
                if (interventionToUpdate == null)
                    return NotFound($"Intervention with Id:{id} not found");

                var reclamation = await reclamationRepo.GetReclamation(dto.ReclamationId);
                if (reclamation == null) return BadRequest("Ivalid reclamation Id");

                var user = await utilisateurRepository.GetUtilisateur(dto.UtilisateurId);
                if (user == null) return BadRequest("Ivalid user Id");

                interventionToUpdate.Intervention_Description = dto.Intervention_Description;
                interventionToUpdate.DateIntervention = dto.DateIntervention;
                interventionToUpdate.CoutManuel = dto.CoutManuel;
                interventionToUpdate.ReclamationId = dto.ReclamationId;
                interventionToUpdate.UtilisateurId = dto.UtilisateurId;
                interventionToUpdate.Statut = dto.Statut;

                await interventionRepo.UpdateIntervention(id, interventionToUpdate);

                return Ok(interventionToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la mise à jour de données : {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteIntervention(int id)
        {
            try
            {
                var intervention = await interventionRepo.GetIntervention(id);
                if (intervention == null)
                    return NotFound($"Intervention with Id:{id} not found");
                return Ok(await interventionRepo.DeleteIntervention(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de la supression de données : {ex.Message}");
            }
        }
    }
}
