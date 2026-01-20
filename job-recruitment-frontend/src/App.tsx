import React, { useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import { Toaster } from 'react-hot-toast';
import { useAuthStore } from './stores/authStore';

// Pages
import LoginPage from './pages/auth/LoginPage';
import RegisterPage from './pages/auth/RegisterPage';
import SeekerHomePage from './pages/seeker/HomePage';
import JobDetailsPage from './pages/seeker/JobDetailsPage';
import ApplicationsPage from './pages/seeker/ApplicationsPage';
import RecruiterHomePage from './pages/recruiter/HomePage';
import PostJobPage from './pages/recruiter/PostJobPage';
import MyJobsPage from './pages/recruiter/MyJobsPage';
import ProfilePage from './pages/common/ProfilePage';

// Components
import ProtectedRoute from './components/common/ProtectedRoute';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
      staleTime: 5 * 60 * 1000, // 5 minutes
    },
  },
});

const App: React.FC = () => {
  const { initialize } = useAuthStore();

  useEffect(() => {
    initialize();
  }, []);

  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <div className="min-h-screen bg-gray-50">
          <Toaster
            position="top-right"
            toastOptions={{
              duration: 4000,
              style: {
                background: '#363636',
                color: '#fff',
              },
              success: {
                duration: 3000,
                style: {
                  background: '#10b981',
                },
              },
              error: {
                duration: 4000,
                style: {
                  background: '#ef4444',
                },
              },
            }}
          />
          
          <Routes>
            {/* Public Routes */}
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            
            {/* Protected Routes - Seeker */}
            <Route path="/seeker" element={
              <ProtectedRoute requiredRole="Seeker">
                <SeekerHomePage />
              </ProtectedRoute>
            } />
            <Route path="/seeker/jobs/:id" element={
              <ProtectedRoute requiredRole="Seeker">
                <JobDetailsPage />
              </ProtectedRoute>
            } />
            <Route path="/seeker/applications" element={
              <ProtectedRoute requiredRole="Seeker">
                <ApplicationsPage />
              </ProtectedRoute>
            } />
            
            {/* Protected Routes - Recruiter */}
            <Route path="/recruiter" element={
              <ProtectedRoute requiredRole="Recruiter">
                <RecruiterHomePage />
              </ProtectedRoute>
            } />
            <Route path="/recruiter/post-job" element={
              <ProtectedRoute requiredRole="Recruiter">
                <PostJobPage />
              </ProtectedRoute>
            } />
            <Route path="/recruiter/my-jobs" element={
              <ProtectedRoute requiredRole="Recruiter">
                <MyJobsPage />
              </ProtectedRoute>
            } />
            
            {/* Common Routes */}
            <Route path="/profile" element={
              <ProtectedRoute>
                <ProfilePage />
              </ProtectedRoute>
            } />
            
            {/* Redirect based on user role */}
            <Route path="/" element={
              <ProtectedRoute>
                <RoleBasedRedirect />
              </ProtectedRoute>
            } />
          </Routes>
        </div>
      </Router>
      <ReactQueryDevtools initialIsOpen={false} />
    </QueryClientProvider>
  );
};

const RoleBasedRedirect: React.FC = () => {
  const { user } = useAuthStore();
  
  if (user?.userType === 'Recruiter') {
    return <Navigate to="/recruiter" replace />;
  }
  
  return <Navigate to="/seeker" replace />;
};

export default App;