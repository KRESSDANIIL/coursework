using System;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class LoginViewModel
    {
        [Key]
        [Required(ErrorMessage = "Введите логин.")]
        [Display(Name = "Логин")]
        public string AdminLogin { get; set; }

        [Required(ErrorMessage = "Введите пароль.")]
        [Display(Name = "Пароль")]
        public string AdminPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}
