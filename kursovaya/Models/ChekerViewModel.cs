using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class ChekerViewModel
    {

        [Required(ErrorMessage = "Необходимо указать тип абонимента")]
        [Display(Name = "Тип абонимента")]
        public string MembershipType { get; set; }
    }
}