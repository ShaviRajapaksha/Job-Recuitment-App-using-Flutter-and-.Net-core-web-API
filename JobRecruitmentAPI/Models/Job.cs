using System.ComponentModel.DataAnnotations;

namespace JobRecruitmentAPI.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string Company { get; set; } = string.Empty;
        
        [Required]
        public string Location { get; set; } = string.Empty;
        
        public decimal Salary { get; set; }
        
        [Required]
        public string EmploymentType { get; set; } = "Full-time"; // Full-time, Part-time, Contract
        
        public string? Requirements { get; set; }
        public string? Benefits { get; set; }
        
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDate { get; set; }
        
        // Foreign key
        public int RecruiterId { get; set; }
        
        // Navigation properties
        public User? Recruiter { get; set; }
        public List<Application>? Applications { get; set; }
    }
}