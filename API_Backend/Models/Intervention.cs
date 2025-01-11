using System.ComponentModel.DataAnnotations;

namespace API_Backend.Models
{
    public class Intervention
    {
        public int InterventionId { get; set; }
        public string Intervention_Description { get; set; }
        public DateTime? DateIntervention { get; set; }
        public bool? EstGratuite { get; set; }
        public double? CoutManuel { get; set; }
        public int ReclamationId { get; set; }
        public Reclamation Reclamation { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public string? Statut {  get; set; } = StatutIntervention.En_attente.ToString();
        //public ICollection<PieceRechange> PiecesEchanges { get; set; }
        public ICollection<PieceUtilise> PieceUtilises { get; set; }
    }
}
