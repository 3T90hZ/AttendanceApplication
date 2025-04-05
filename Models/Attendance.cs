namespace AttendanceApplication.Models
{
    public enum AttendanceStatus
    {
        Present,
        AbsentWithPermission,
        AbsentWithoutPermission
    }

    public class Attendance
    {
        public int Id { get; set; }
        public int ClassSessionId { get; set; }
        public ClassSession ClassSession { get; set; }
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        public AttendanceStatus Status { get; set; }
        public DateTime? CheckInTime { get; set; }
    }
}
