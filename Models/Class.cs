namespace AttendanceApplication.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., "Mathematics 101"
        public string TeacherId { get; set; }
        public ApplicationUser Teacher { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new();
        public List<ClassSession> ClassSessions { get; set; } = new();
    }
}
