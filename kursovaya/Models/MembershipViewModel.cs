using Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class MembershipViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо указать тип абонимента")]
        [Display(Name = "Тип абонимента")]
        public string MembershipType { get; set; }
        [Required(ErrorMessage = "Необходимо указать цену")]
        [Display(Name = "Цена")]
        public float Cost { get; set; }
      
    }
}
