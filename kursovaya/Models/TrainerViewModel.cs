using Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class TrainerViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо указать имя")]
        [Display(Name = "Имя")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Необходимо указать Фамилию")]
        [Display(Name = "Фамилия")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Необходимо указать E-mail")]
        [Display(Name = "E-mail")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Необходимо указать телефон")]
        [Display(Name = "Телефон")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Необходимо указать специальность")]
        [Display(Name = "Специальность")]

        public required string Specialization { get; set; }
    }
}
