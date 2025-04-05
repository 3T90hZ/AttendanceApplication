namespace AttendanceApplication.Models
{
    public class StudentProfile
    {
        public string UserId { get; set; }
        public string StudentId { get; set; } // e.g., "STU12345"
        public ApplicationUser User { get; set; }
    }
}
