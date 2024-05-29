using Data;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.Models
{
    public class PaymentViewModel
    {
            [Key]
            public int Id { get; set; }

            [Required]
            public int MembershipId { get; set; }

            [Required]
            public DateTime PaymentDate { get; set; }

            [Required]
            public float PaymentAmount { get; set; }

        }
}