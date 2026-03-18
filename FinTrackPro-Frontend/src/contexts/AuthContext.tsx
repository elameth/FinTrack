import { createContext, useContext, useState, type ReactNode } from 'react'
import apiClient from '../api/axios.ts'

interface User {
    id: string
    email: string
    firstName: string
    lastName: string
}

interface AuthTokenResponse {
    accessToken: string
    refreshToken: string
    expiresInMinutes: number
}

interface AuthContextType {
    user: User | null
    isAuthenticated: boolean
    login: (email: string, password: string) => Promise<void>
    register: (email: string, password: string, firstName: string, lastName: string) => Promise<void>
    logout: () => void
}

function decodeTokenPayload(token: string): User {
    const payload = token.split('.')[1]
    const json = atob(payload.replace(/-/g, '+').replace(/_/g, '/'))
    const claims = JSON.parse(json)

    return {
        id: claims.sub,
        email: claims.email,
        firstName: claims.given_name,
        lastName: claims.family_name,
    }
}

const AuthContext = createContext<AuthContextType | null>(null)

export function AuthProvider({ children }: { children: ReactNode }) {
    // Restore session from localStorage on first render
    const [user, setUser] = useState<User | null>(() => {
        const token = localStorage.getItem('accessToken')
        if (token) {
            try {
                return decodeTokenPayload(token)
            } catch {
                localStorage.removeItem('accessToken')
                localStorage.removeItem('refreshToken')
                return null
            }
        }
        return null
    })

    const isAuthenticated = !!user

    function handleAuthResponse(response: AuthTokenResponse) {
        localStorage.setItem('accessToken', response.accessToken)
        localStorage.setItem('refreshToken', response.refreshToken)
        setUser(decodeTokenPayload(response.accessToken))
    }

    async function login(email: string, password: string) {
        const response = await apiClient.post<AuthTokenResponse>('/auth/login', {
            email,
            password,
        })
        handleAuthResponse(response.data)
    }

    async function register(email: string, password: string, firstName: string, lastName: string) {
        const response = await apiClient.post<AuthTokenResponse>('/auth/register', {
            email,
            password,
            firstName,
            lastName,
        })
        handleAuthResponse(response.data)
    }

    function logout() {
        localStorage.removeItem('accessToken')
        localStorage.removeItem('refreshToken')
        setUser(null)
    }

    return (
        <AuthContext.Provider value={{ user, isAuthenticated, login, register, logout }}>
            {children}
        </AuthContext.Provider>
    )
}

export function useAuth() {
    const context = useContext(AuthContext)
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider')
    }
    return context
}
