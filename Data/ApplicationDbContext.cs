namespace AttendanceApplication.Data
{
    using AttendanceApplication.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<ClassSession> ClassSessions { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure StudentProfile entity
            builder.Entity<StudentProfile>(entity =>
            {
                entity.HasKey(sp => sp.UserId);
                entity.HasOne(sp => sp.User)
                      .WithOne(u => u.StudentProfile)
                      .HasForeignKey<StudentProfile>(sp => sp.UserId);
            });

            // Configure Class entity relationships
            builder.Entity<Class>()
                .HasOne(c => c.Teacher)
                .WithMany()
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete from Teacher

            // Configure Enrollment entity relationships
            builder.Entity<Enrollment>()
                .HasOne(e => e.Class)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascade from Class to Enrollment

            builder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete from Student

            // Configure ClassSession entity relationships
            builder.Entity<ClassSession>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassSessions)
                .HasForeignKey(cs => cs.ClassId)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascade from Class to ClassSession

            // Configure Attendance entity relationships
            builder.Entity<Attendance>()
                .HasOne(a => a.ClassSession)
                .WithMany(cs => cs.Attendances)
                .HasForeignKey(a => a.ClassSessionId)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascade from ClassSession to Attendance

            builder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete from Student
        }
    }
}
