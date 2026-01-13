using JobRecruitmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobRecruitmentAPI.Data
{
    public class AppDbContext : DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Application> Applications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Job relationship (Recruiter posts jobs)
            modelBuilder.Entity<User>()
                .HasMany(u => u.PostedJobs)
                .WithOne(j => j.Recruiter)
                .HasForeignKey(j => j.RecruiterId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // User-Application relationship (Seeker applies)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Applications)
                .WithOne(a => a.Seeker)
                .HasForeignKey(a => a.SeekerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Job-Application relationship
            modelBuilder.Entity<Job>()
                .HasMany(j => j.Applications)
                .WithOne(a => a.Job)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}