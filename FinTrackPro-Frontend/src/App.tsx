import { Routes, Route, Navigate } from 'react-router-dom'
import { useAuth } from './contexts/AuthContext.tsx'
import LoginPage from './pages/LoginPage.tsx'
import RegisterPage from './pages/RegisterPage.tsx'

// --- ROUTE GUARD ---
// A small component that protects routes. If the user is not authenticated,
// it redirects to /login. "replace" means this redirect won't appear in
// browser history (hitting "back" won't loop you back to the redirect).
function ProtectedRoute({ children }: { children: React.ReactNode }) {
    const { isAuthenticated } = useAuth()
    if (!isAuthenticated) {
        return <Navigate to="/login" replace />
    }
    return children
}

// --- HOME PAGE (placeholder) ---
// Just proves that auth works. Shows the logged-in user's info and a logout button.
function HomePage() {
    const { user, logout } = useAuth()

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-50 p-4">
            <div className="bg-white rounded-lg shadow-md p-8 max-w-md w-full text-center">
                <h1 className="text-2xl font-bold text-gray-800 mb-2">
                    Welcome, {user?.firstName}!
                </h1>
                <p className="text-gray-600 mb-6">{user?.email}</p>
                <button
                    onClick={logout}
                    className="px-4 py-2 bg-red-600 text-white rounded-md hover:bg-red-700
                               transition-colors"
                >
                    Sign out
                </button>
            </div>
        </div>
    )
}

// --- APP (root component) ---
// Routes is react-router's router — it looks at the current URL and renders
// the matching Route's element. Think of it like a switch statement on the URL path.
export default function App() {
    return (
        <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route
                path="/"
                element={
                    <ProtectedRoute>
                        <HomePage />
                    </ProtectedRoute>
                }
            />
        </Routes>
    )
}
