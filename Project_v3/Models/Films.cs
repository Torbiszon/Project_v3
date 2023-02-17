using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Project_v3.Models
{
    public class Films
    {
        [Key]
        public int FilmId { get; set; }
        [Required]
        [Display (Name = "Tytuł")]
        public string FilmName { get; set;}
        [Display (Name = "Opis")]
        public string FilmDescription { get; set;}
        [Display (Name = "Gatunek")]
        public string FilmType { get; set;}
        [Display (Name = "Ocena")]
        public int FilmCount { get; set;}
        [Display (Name = "Reżyser")]
        public int DirectorId { get; set; }

        public List<AppUser> Users { get; set;}
    }
}
