using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using JobRecruitmentAPI.Data;
using JobRecruitmentAPI.Models;
using JobRecruitmentAPI.DTOs;
using System.Security.Claims;

namespace JobRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/applications/{jobId} - Apply for a job
        [HttpPost("{jobId}")]
        public async Task<ActionResult> ApplyForJob(int jobId, ApplyJobDTO applyJobDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var userType = User.FindFirst(ClaimTypes.Role)!.Value;

            if (userType != "Seeker")
                return Forbid("Only job seekers can apply for jobs");

            // Check if job exists
            var job = await _context.Jobs.FindAsync(jobId);
            if (job == null || job.ExpiryDate < DateTime.UtcNow)
                return NotFound("Job not found or expired");

            // Check if already applied
            var existingApplication = await _context.Applications
                .FirstOrDefaultAsync(a => a.JobId == jobId && a.SeekerId == userId);
            
            if (existingApplication != null)
                return BadRequest("You have already applied for this job");

            var application = new Application
            {
                JobId = jobId,
                SeekerId = userId,
                CoverLetter = applyJobDTO.CoverLetter,
                ResumeUrl = applyJobDTO.ResumeUrl,
                AppliedDate = DateTime.UtcNow,
                Status = "Pending"
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Application submitted successfully" });
        }

        // GET: api/applications/my-applications - Get applications by current seeker
        [HttpGet("my-applications")]
        public async Task<ActionResult> GetMyApplications()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var userType = User.FindFirst(ClaimTypes.Role)!.Value;

            if (userType != "Seeker")
                return Forbid("Only job seekers can view their applications");

            var applications = await _context.Applications
                .Include(a => a.Job)
                .Where(a => a.SeekerId == userId)
                .OrderByDescending(a => a.AppliedDate)
                .Select(a => new
                {
                    a.Id,
                    JobTitle = a.Job!.Title,
                    JobCompany = a.Job.Company,
                    a.CoverLetter,
                    a.ResumeUrl,
                    a.Status,
                    a.AppliedDate
                })
                .ToListAsync();

            return Ok(applications);
        }
    }
}