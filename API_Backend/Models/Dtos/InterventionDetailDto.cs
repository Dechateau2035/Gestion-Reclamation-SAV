using System.ComponentModel.DataAnnotations;

namespace API_Backend.Models.Dtos
{
    public class InterventionDetailDto
    {
        public int InterventionId { get; set; }
        public string Intervention_Description { get; set; }
        public DateTime? DateIntervention { get; set; }
        public bool? EstGratuite { get; set; }
        public double? CoutManuel { get; set; }
        public int ReclamationId { get; set; }
        public string ReclamationDesc { get; set; }
        public int UtilisateurId { get; set; }
        public string UtilisateurNom { get; set; }
        public string? Statut { get; set; }
        public ICollection<PieceRechange> PiecesEchanges { get; set; }
    }
}
