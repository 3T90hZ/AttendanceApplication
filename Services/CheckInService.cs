using AttendanceApplication.Data;
using AttendanceApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApplication.Services
{
    // Services/CheckInService.cs
    public class CheckInService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckInService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<(bool Success, string Message)> CheckInAsync(string code, string studentUserId)
        {
            var classSession = await _context.ClassSessions
                .FirstOrDefaultAsync(cs => cs.CheckInCode == code && cs.DateTime.Date == DateTime.Today);

            if (classSession == null)
                return (false, "Invalid or expired check-in code.");

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.ClassId == classSession.ClassId && e.StudentId == studentUserId);

            if (enrollment == null)
                return (false, "You are not enrolled in this class.");

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(a => a.ClassSessionId == classSession.Id && a.StudentId == studentUserId);

            if (attendance == null)
            {
                attendance = new Attendance
                {
                    ClassSessionId = classSession.Id,
                    StudentId = studentUserId,
                    Status = AttendanceStatus.Present,
                    CheckInTime = DateTime.Now
                };
                _context.Attendances.Add(attendance);
            }
            else if (attendance.Status != AttendanceStatus.Present)
            {
                attendance.Status = AttendanceStatus.Present;
                attendance.CheckInTime = DateTime.Now;
            }
            else
            {
                return (false, "You have already checked in.");
            }

            await _context.SaveChangesAsync();
            return (true, "Check-in successful.");
        }
    }
}
