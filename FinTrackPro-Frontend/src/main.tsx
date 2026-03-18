import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import { AuthProvider } from './contexts/AuthContext.tsx'
import './index.css'
import App from './App.tsx'

// This is the entry point — the very first code that runs.
//
// The nesting order matters:
//   StrictMode   → React dev tool that warns about common mistakes (dev only, no effect in prod)
//   BrowserRouter → Enables URL-based routing (must wrap anything that uses Link, Routes, useNavigate)
//   AuthProvider  → Makes auth state (user, login, logout) available to all components inside
//   App           → Your actual UI with routes
//
// createRoot finds the <div id="root"> in index.html and mounts the React app into it.
createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <BrowserRouter>
            <AuthProvider>
                <App />
            </AuthProvider>
        </BrowserRouter>
    </StrictMode>,
)
