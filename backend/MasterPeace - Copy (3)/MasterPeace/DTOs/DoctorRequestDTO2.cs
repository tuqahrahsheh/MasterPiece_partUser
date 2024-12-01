using System.ComponentModel.DataAnnotations;

namespace MasterPeace.DTOs
{
    
        public class DoctorRequestDTO2

        {
         public int RequestId { get; set; }

    public string Name { get; set; } = null!;

    public string Specialty { get; set; } = null!;

    public string? Description { get; set; }

    public string ContactEmail { get; set; } = null!;

    public DateTime? RequestDate { get; set; }

        public IFormFile? DoctorImage { get; set; }

        public string Status { get; set; } = null!;
        }
    }
