using System.ComponentModel.DataAnnotations;

namespace API_Backend.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nom { get; set; }
        public string Description { get; set; }
        [Required]
        public bool? SousGarantie { get; set; }
        public DateTime? DateAchat { get; set; }
        [Required]
        public double? Prix { get; set; }
    }
}
