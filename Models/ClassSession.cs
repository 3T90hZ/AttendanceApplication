namespace AttendanceApplication.Models
{
    public class ClassSession
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public DateTime DateTime { get; set; }
        public string CheckInCode { get; set; } // e.g., "123456"
        public List<Attendance> Attendances { get; set; } = new();
    }
}
