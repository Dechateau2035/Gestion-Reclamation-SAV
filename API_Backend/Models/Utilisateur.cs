using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace API_Backend.Models
{
    public class Utilisateur
    {
        public int UtilisateurId { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Telephone { get; set; }
        [Required]
        public string Adresse { get; set; }
        public string Type_Utilisateur { get; set; } = TypeUser.Client.ToString();
    }
}
