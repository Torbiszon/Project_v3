using System.ComponentModel.DataAnnotations;

namespace Project_v3.Models
{

    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
