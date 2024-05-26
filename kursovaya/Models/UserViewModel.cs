using Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо указать логин администратора")]
        [Display(Name = "Логин")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Необходимо указать пароль администратора")]
        [Display(Name = "Пароль")]
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}