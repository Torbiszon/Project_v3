using System.ComponentModel.DataAnnotations;

namespace Project_v3.Models
{
    public class Films
    {
        [Key]
        public int FilmId { get; set; }
        [Required]
        public string FilmName { get; set;}
        public string FilmDescription { get; set;}
        public string FilmType { get; set;}
        public int FilmCount { get; set;}
        public int DirectorId { get; set; }
    }
}
