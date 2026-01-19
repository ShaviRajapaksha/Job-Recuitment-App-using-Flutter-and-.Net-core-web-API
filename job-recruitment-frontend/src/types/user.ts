export interface User {
    id: number;
    email: string;
    fullName: string;
    userType: "Recruiter" | "Seeker";
    phone?: string;
    profileImage?: string;
    bio?: string;
    token: string;
}

export interface RegisterData {
    email: string;
    password: string;
    fullName: string;
    userType: 'Recruiter' | 'Seeker';
    phone?: string;
}

export interface LoginData {
    email: string;
    password: string;
}

export interface AuthResponse {
    id: number;
    email: string;
    fullName: string;
    token: string;
}