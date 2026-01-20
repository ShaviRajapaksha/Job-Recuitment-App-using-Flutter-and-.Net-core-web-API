export const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';

export const ROUTES = {
    HOME: '/',
    LOGIN: '/login',
    REGISTER: '/register',
    PROFILE: '/profile',
    SEEKER: {
        HOME: '/seeker',
        JOBS: '/seeker/jobs',
        JOB_DETAILS: '/seeker/jobs/:id',
        APPLICATIONS: '/seeker/appilications',
    },
    RECRUITER: {
        HOME: '/recruiter',
        POST_JOB: '/recruiter/post-job',
        MY_JOBS: '/recruiter/my-jobs',
    },
};

export const EMPLOYMENT_TYPES = [
    'Full-time',
    'Part-time',
    'Contract',
    'Internship',
    'Remote',
];

export const APPLICATION_STATUS = {
    PENDING: 'Pending',
    REVIEWED: 'Reviewed',
    ACCEPTED: 'Accepted',
    REJECTED: 'Rejected',
} as const;

export const APPLICATION_STATUS_COLORS = {
  [APPLICATION_STATUS.PENDING]: 'bg-yellow-100 text-yellow-800',
  [APPLICATION_STATUS.REVIEWED]: 'bg-blue-100 text-blue-800',
  [APPLICATION_STATUS.ACCEPTED]: 'bg-green-100 text-green-800',
  [APPLICATION_STATUS.REJECTED]: 'bg-red-100 text-red-800',    
}

export const APPLICATION_STATUS_ICONS = {
  [APPLICATION_STATUS.PENDING]: 'Clock',
  [APPLICATION_STATUS.REVIEWED]: 'Eye',
  [APPLICATION_STATUS.ACCEPTED]: 'CheckCircle',
  [APPLICATION_STATUS.REJECTED]: 'XCircle',
};

