using Microsoft.Build.Framework;
using System.Drawing;
using System.Security.Permissions;

namespace Project_v3.Models
{
    public class CreateFilm
    {
        [Required]
        public string FilmName { get; set; }
        [Required]
        public string FilmDescription { get; set;}
        [Required]
        public string FilmType { get; set; }

        public string selectedDirector { get; set; }
        public List<string> FullNames { get;set; }
    }
}
