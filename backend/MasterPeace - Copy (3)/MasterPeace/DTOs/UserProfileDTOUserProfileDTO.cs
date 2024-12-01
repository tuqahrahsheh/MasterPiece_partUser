namespace MasterPeace.DTOs
{
    public class UserProfileDTOUserProfileDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

        // List of appointments for the user
        public List<AppointmentDTO> Appointments { get; set; }
    }
}
