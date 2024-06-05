using Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class SessionViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо указать id тренера")]
        [Display(Name = "Id Тренера")]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Необходимо указать дату")]
        [Display(Name = "Дата и время")]
        public DateTime SessionDate { get; set; }


        [Required(ErrorMessage = "Необходимо указать длительность")]
        [Display(Name = "Длительность")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Необходимо указать тип")]
        [Display(Name = "Тип")]
        public string SessionType { get; set; }

        [Required(ErrorMessage = "Необходимо указать нужный пропуск")]
        [Display(Name = "Id Пропуска")]
        public int MembershipId { get; set; }
    }
}
