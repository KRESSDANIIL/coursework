using Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class MembershipViewModel
    {
        [Key]
        public int Id { get; set; }

        
            public int ClientId { get; set; }
            public string MembershipType { get; set; }
            public decimal Cost { get; set; }
            public virtual Client Client { get; set; }
            public virtual ICollection<Session> Sessions { get; set; }
            public virtual ICollection<Payment> Payments { get; set; }
        [Required(ErrorMessage = "Необходимо указать id тренера")]
        [Display(Name = "Id Тренера")]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Необходимо указать дату")]
        [Display(Name = "Дата и время")]
        public DateTime SessionDate { get; set; }

        [Required(ErrorMessage = "Необходимо указать время начала")]
        [Display(Name = "Время начала")]
        public TimeSpan SessionTime { get; set; }

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
