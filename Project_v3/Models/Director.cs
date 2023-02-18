using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Project_v3.Models
{
    public class Director
    {
        public int Id { get; set; }
        [Display (Name = "Imie")]
        public string Name { get; set; }
        [Display (Name = "Nazwisko")]
        public string Surname { get; set; }
        public string fullname { get; set; }
        public  List<Films> Films { get; set; }
    }
}
