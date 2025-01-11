using System.ComponentModel.DataAnnotations;

namespace API_Backend.Models
{
    public class Reclamation
    {
        public int ReclamationId { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? DateSoumission { get; set; }
        public String? Etat_Reclamation { get; set; } = EtatReclamation.En_attente_information.ToString();
        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public ICollection<Intervention> Interventions { get; set; }
    }
}
