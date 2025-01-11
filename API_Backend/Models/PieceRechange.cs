using System.ComponentModel.DataAnnotations;

namespace API_Backend.Models
{
    public class PieceRechange
    {
        public int PieceRechangeId { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public double? Prix { get; set; }
        [Required]
        public int? StockDisponible { get; set; }
        public ICollection<PieceUtilise> PiecesUtilises { get; set; }
    }
}
