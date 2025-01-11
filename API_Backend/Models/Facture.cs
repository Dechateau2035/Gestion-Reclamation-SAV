namespace API_Backend.Models
{
    public class Facture
    {
        public int FactureId { get; set; }
        public DateTime? DateFacture { get; set; } = DateTime.Now;
        public double? MontantTotal { get; set; }
        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; }
        public string Statut { get; set; } = StatutFacture.Non_payée.ToString();
    }
}