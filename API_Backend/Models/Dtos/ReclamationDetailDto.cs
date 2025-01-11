using System.ComponentModel.DataAnnotations;

namespace API_Backend.Models.Dtos
{
    public class ReclamationDetailDto
    {
        public int ReclamationId { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? DateSoumission { get; set; }
        public String Etat_Reclamation { get; set; }
        public int UtilisateurId { get; set; }
        public string UtilisateurNom { get; set; }
        public int ArticleId { get; set; }
        public String ArticleNom { get; set; }
        public ICollection<int> InterventionIds { get; set; }
    }
}
