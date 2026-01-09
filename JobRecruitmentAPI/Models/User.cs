using System.ComponentModel.DataAnnotations;

namespace JobRecruitmentAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string UserType {get; set; } = "Seeker";

        public string? Phone { get; set; }
        public string? ProfileImage { get; set; }
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public List<Job>? PostedJobs { get; set; }
        public List<Application>? Applications { get; set; }
    }
}