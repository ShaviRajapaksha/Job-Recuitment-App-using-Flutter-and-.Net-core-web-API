namespace JobRecruitmentAPI.DTOs
{
    public class CreateJobDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string EmploymentType { get; set; } = "Full-time";
        public string? Requirements { get; set; }
        public string? Benefits { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class JobResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string EmploymentType { get; set; } = string.Empty;
        public string? Requirements { get; set; }
        public string? Benefits { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string RecruiterName { get; set; } = string.Empty;
    }

    public class ApplyJobDTO
    {
        public string CoverLetter { get; set; } = string.Empty;
        public string? ResumeUrl { get; set; }
    }
}