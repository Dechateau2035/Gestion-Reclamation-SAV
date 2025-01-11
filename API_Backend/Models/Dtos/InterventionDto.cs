namespace API_Backend.Models.Dtos
{
    public class InterventionDto
    {
        public string Intervention_Description { get; set; }
        public DateTime? DateIntervention { get; set; }
        public double? CoutManuel { get; set; }
        public int ReclamationId { get; set; }
        public int UtilisateurId { get; set; }
        public string? Statut { get; set; }
    }
}
