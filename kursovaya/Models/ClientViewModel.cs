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
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Неправильный формат эл почты")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Необходимо указать телефон")]
        [Display(Name = "Телефон")]
        [RegularExpression(@"^(\+7)[0-9]{10}$", ErrorMessage = "Неправильный формат номера телефона. Ожидалось +7XXXXXXXXXX.")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Необходимо указать дату рождения")]
        [Display(Name = "Дата рождения")]

        public required DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Необходимо указать пол")]
        [Display(Name = "Пол")]
        [RegularExpression(@"^(М|Ж|)$", ErrorMessage = "Введите данные в бодходящем формате (М|Ж|).")]
        public required string Gender { get; set; }

        [Required(ErrorMessage = "Необходимо указать Тип абонемента")]
        [Display(Name = "Тип абонемента")]
        public string MembershipType { get; set; }

        [Required(ErrorMessage = "Необходимо указать дату начала абанимента")]
        [Display(Name = "Дата начала абонимент")]
        public DateOnly MembershipStartDate { get; set; }
        [Required(ErrorMessage = "Необходимо указать дату окончания абанимента")]
        [Display(Name = "Дата окончания абанимента")]
        public DateOnly MembershipEndDate { get; set; }

    }
}
