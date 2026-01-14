using System.ComponentModel.DataAnnotations;

namespace JobRecruitmentAPI.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string CoverLetter { get; set; } = string.Empty;
        
        public string? ResumeUrl { get; set; }
        
        public string Status { get; set; } = "Pending"; // Pending, Reviewed, Accepted, Rejected
        
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
        
        // Foreign keys
        public int JobId { get; set; }
        public int SeekerId { get; set; }
        
        // Navigation properties
        public Job? Job { get; set; }
        public User? Seeker { get; set; }
    }
}