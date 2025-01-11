namespace API_Backend.Models
{
    public class PieceUtilise
    {
        public int PieceUtiliseId { get; set; }
        public int Quantite { get; set; }
        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; }
        public int PieceRechangeId { get; set; }
        public PieceRechange PieceRechange { get; set; }
    }
}
