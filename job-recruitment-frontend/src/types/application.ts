export interface Application {
    id: number;
    jobId: number;
    seekerId: number;
    coverLetter: string;
    resumeUrl?: string;
    status: 'Pending' | 'Reviewed' | 'Accepted' | 'Rekected' ;
    appliedDate: string;
    jobTitle?: string;
    jobCompany?: string;
}

export interface ApplyJobData {
    coverLetter: string;
    resumeUrl?: string;
}