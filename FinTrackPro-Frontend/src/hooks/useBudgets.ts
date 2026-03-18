import { useCallback, useEffect, useState } from 'react'
import apiClient from '../api/axios.ts'
import axios from 'axios'
import type { BudgetDto, CreateBudgetRequest } from '../types/budget.ts'

export function useBudgets() {
    const [budgets, setBudgets] = useState<BudgetDto[]>([])
    const [isLoading, setIsLoading] = useState(true)
    const [error, setError] = useState('')

    const fetchBudgets = useCallback(async () => {
        setIsLoading(true)
        setError('')

        try {
            const response = await apiClient.get<BudgetDto[]>('/budgets')
            setBudgets(response.data)
        } catch (err) {
            if (axios.isAxiosError(err)) {
                setError(err.response?.data?.detail ?? 'Failed to load budgets.')
            } else {
                setError('Something went wrong. Please try again.')
            }
        } finally {
            setIsLoading(false)
        }
    }, [])

    useEffect(() => {
        fetchBudgets()
    }, [fetchBudgets])

    async function createBudget(request: CreateBudgetRequest): Promise<void> {
        const response = await apiClient.post<BudgetDto>('/budgets', request)
        setBudgets((previous) => [...previous, response.data])
    }

    return { budgets, isLoading, error, createBudget, refreshBudgets: fetchBudgets }
}
