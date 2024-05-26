using Microsoft.AspNetCore.Mvc;

namespace Kursovaya.Models
{
        public class ErrorViewModel
        {
            public string? RequestId { get; set; }

            public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        }
}
