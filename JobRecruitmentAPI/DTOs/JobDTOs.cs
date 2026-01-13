namespace JobRecruitmentAPI.DTOs
{
    public class CreateJobDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string EmploymentType { get; set; } = "Full-Time";
        public string? Requirements { get; set; }
        public string? Benefits { get; set; }
        public DateTime? ExpiryDate { get; set; }  
    }
}