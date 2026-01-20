import api from './api';
import { setToken, setUser, clearAuth, getToken, getUser } from '../utils/helpers';
import type { AuthResponse, LoginData, RegisterData, User } from '../types/user';


class AuthService {
  async register(data: RegisterData): Promise<User> {
    try {
      const response = await api.post<AuthResponse>('/auth/register', data);
      setToken(response.token);
      const user: User = {
        id: response.id,
        email: response.email,
        fullName: response.fullName,
        userType: response.userType as 'Recruiter' | 'Seeker',
        token: response.token,
      };
      setUser(user);
      return user;
    } catch (error) {
      clearAuth();
      throw error;
    }
  }

  async login(data: LoginData): Promise<User> {
    try {
      const response = await api.post<AuthResponse>('/auth/login', data);
      setToken(response.token);
      const user: User = {
        id: response.id,
        email: response.email,
        fullName: response.fullName,
        userType: response.userType as 'Recruiter' | 'Seeker',
        token: response.token,
      };
      setUser(user);
      return user;
    } catch (error) {
      clearAuth();
      throw error;
    }
  }

  logout(): void {
    clearAuth();
  }

  getCurrentUser(): User | null {
    return getUser();
  }

  isAuthenticated(): boolean {
    return !!getToken();
  }

  isRecruiter(): boolean {
    const user = this.getCurrentUser();
    return user?.userType === 'Recruiter';
  }

  isSeeker(): boolean {
    const user = this.getCurrentUser();
    return user?.userType === 'Seeker';
  }
}

export default new AuthService();

