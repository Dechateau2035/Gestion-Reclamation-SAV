using System.ComponentModel.DataAnnotations;

namespace API_Backend.Models.Dtos
{
    public class ReclamationDto
    {
        [Required]
        public string Description { get; set; }
        public DateTime? DateSoumission { get; set; }
        public String Etat_Reclamation { get; set; } = EtatReclamation.En_attente_information.ToString();
        public int UtilisateurId { get; set; }
        public int ArticleId { get; set; }
    }
}
