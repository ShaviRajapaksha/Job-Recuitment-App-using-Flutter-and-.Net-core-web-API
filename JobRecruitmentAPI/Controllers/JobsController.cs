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
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/jobs - Get all jobs (for seekers)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobResponseDTO>>> GetJobs()
        {
            var jobs = await _context.Jobs
                .Include(j => j.Recruiter)
                .Where(j => j.ExpiryDate > DateTime.UtcNow)
                .OrderByDescending(j => j.PostedDate)
                .Select(j => new JobResponseDTO
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    Company = j.Company,
                    Location = j.Location,
                    Salary = j.Salary,
                    EmploymentType = j.EmploymentType,
                    Requirements = j.Requirements,
                    Benefits = j.Benefits,
                    PostedDate = j.PostedDate,
                    ExpiryDate = j.ExpiryDate,
                    RecruiterName = j.Recruiter!.FullName
                })
                .ToListAsync();

            return Ok(jobs);
        }

        // POST: api/jobs - Create new job (for recruiters only)
        [HttpPost]
        public async Task<ActionResult<Job>> CreateJob(CreateJobDTO createJobDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var userType = User.FindFirst(ClaimTypes.Role)!.Value;

            if (userType != "Recruiter")
                return Forbid("Only recruiters can post jobs");

            var job = new Job
            {
                Title = createJobDTO.Title,
                Description = createJobDTO.Description,
                Company = createJobDTO.Company,
                Location = createJobDTO.Location,
                Salary = createJobDTO.Salary,
                EmploymentType = createJobDTO.EmploymentType,
                Requirements = createJobDTO.Requirements,
                Benefits = createJobDTO.Benefits,
                ExpiryDate = createJobDTO.ExpiryDate,
                RecruiterId = userId,
                PostedDate = DateTime.UtcNow
            };

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobs), new { id = job.Id }, job);
        }

        // GET: api/jobs/my-jobs - Get jobs posted by current recruiter
        [HttpGet("my-jobs")]
        public async Task<ActionResult<IEnumerable<Job>>> GetMyJobs()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var userType = User.FindFirst(ClaimTypes.Role)!.Value;

            if (userType != "Recruiter")
                return Forbid("Only recruiters can view their jobs");

            var jobs = await _context.Jobs
                .Where(j => j.RecruiterId == userId)
                .OrderByDescending(j => j.PostedDate)
                .ToListAsync();

            return Ok(jobs);
        }
    }
}