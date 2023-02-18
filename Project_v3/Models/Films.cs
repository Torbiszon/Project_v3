using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Project_v3.Models
{
    public class Films
    {
        
        public int Id { get; set; }
        [Required]
        [Display (Name = "Tytuł")]
        public string FilmName { get; set;}
        [Display (Name = "Opis")]
        public string FilmDescription { get; set;}
        [Display (Name = "Gatunek")]
        public string FilmType { get; set;}
        [Display (Name = "Ocena")]
        public int FilmCount { get; set;}
       
        public string Directorfullname { get; set;}
     
        public Director Director { get; set; }

        public List<AppUser> Users { get; set;}
    }
}
