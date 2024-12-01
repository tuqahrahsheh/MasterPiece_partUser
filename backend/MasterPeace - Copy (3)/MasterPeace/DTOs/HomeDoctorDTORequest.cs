namespace MasterPeace.DTOs
{
    public class HomeDoctorDTORequest
    {
        public string FullName { get; set; } = null!;

        public string Specialty { get; set; } = null!;

        public int ExperienceYears { get; set; }

        public string? PhoneNumber { get; set; }

        public double? Rating { get; set; }

        public string? Availability { get; set; }

    }
}
