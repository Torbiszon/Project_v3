using System.ComponentModel.DataAnnotations;

namespace Project_v3.Models
{
    public class EditFilm
    {

        public string FilmName { get; set; }
        [Display(Name = "Opis")]
        public string FilmDescription { get; set; }
        [Display(Name = "Gatunek")]
        public string FilmType { get; set; }
        
    }
}
