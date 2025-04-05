namespace AttendanceApplication.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
    }
}
