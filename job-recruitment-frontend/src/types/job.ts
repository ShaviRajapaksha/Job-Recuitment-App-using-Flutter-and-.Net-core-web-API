export interface Job {
  id: number;
  title: string;
  description: string;
  company: string;
  location: string;
  salary: number;
  employmentType: string;
  requirements?: string;
  benefits?: string;
  postedDate: string;
  expiryDate: string;
  recruiterName: string;
  recruiterId: number;
}

export interface CreateJobData {
  title: string;
  description: string;
  company: string;
  location: string;
  salary: number;
  employmentType: string;
  requirements?: string;
  benefits?: string;
  expiryDate: string;
}

export interface JobFilters {
  search?: string;
  location?: string;
  employmentType?: string;
  minSalary?: number;
  maxSalary?: number;
}