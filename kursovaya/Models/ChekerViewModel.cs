using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class ChekerViewModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}