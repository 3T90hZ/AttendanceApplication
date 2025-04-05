using Microsoft.AspNetCore.Identity;

namespace AttendanceApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public StudentProfile? StudentProfile { get; set; }
    }
}
