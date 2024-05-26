using Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class ClientViewModel
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

        [Required(ErrorMessage = "Необходимо указать дату рождения")]
        [Display(Name = "Дата рождения")]

        public required DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Необходимо указать пол")]
        [Display(Name = "Пол")]

        public required string Gender { get; set; }

        [Required(ErrorMessage = "Необходимо указать Тип членаства")]
        [Display(Name = "Тип членства")]
        public string MembershipType { get; set; }

        [Required(ErrorMessage = "Необходимо указать дату получения членства")]
        [Display(Name = "Дата получения членства")]
        public DateTime MembershipStartDate { get; set; }

        [Required(ErrorMessage = "Необходимо указать дату окончания членства")]
        [Display(Name = "Дата окончания членства")]
        public DateTime MembershipEndDate { get; set; }
    }
}
