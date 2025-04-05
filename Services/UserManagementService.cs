using AttendanceApplication.Data;
using AttendanceApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApplication.Services
{
    // Services/UserManagementService.cs
    public class UserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserManagementService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> CreateUserAsync(string username, string email, string displayName,
            string password, string role, string? studentId = null)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                DisplayName = displayName
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return result;

            await _userManager.AddToRoleAsync(user, role);

            if (role == "Student" && studentId != null)
            {
                var studentProfile = new StudentProfile
                {
                    UserId = user.Id,
                    StudentId = studentId
                };
                _context.StudentProfiles.Add(studentProfile);
                await _context.SaveChangesAsync();
            }

            return result;
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Check for dependencies
            var isTeacher = await _context.Classes.AnyAsync(c => c.TeacherId == userId);
            var isStudent = await _context.Enrollments.AnyAsync(e => e.StudentId == userId);
            if (isTeacher || isStudent)
            {
                // Handle dependencies (e.g., throw exception or clean up)
                throw new InvalidOperationException("Cannot delete user with existing class or enrollment records.");
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
