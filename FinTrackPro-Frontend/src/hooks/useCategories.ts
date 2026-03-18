import { useCallback, useEffect, useState } from 'react'
import apiClient from '../api/axios.ts'
import axios from 'axios'
import type { CategoryDto, CreateCategoryRequest } from '../types/category.ts'

export function useCategories() {
    const [categories, setCategories] = useState<CategoryDto[]>([])
    const [isLoading, setIsLoading] = useState(true)
    const [error, setError] = useState('')

    const fetchCategories = useCallback(async () => {
        setIsLoading(true)
        setError('')

        try {
            const response = await apiClient.get<CategoryDto[]>('/categories')
            setCategories(response.data.filter((category) => category.isActive))
        } catch (err) {
            if (axios.isAxiosError(err)) {
                setError(err.response?.data?.detail ?? 'Failed to load categories.')
            } else {
                setError('Something went wrong. Please try again.')
            }
        } finally {
            setIsLoading(false)
        }
    }, [])

    useEffect(() => {
        fetchCategories()
    }, [fetchCategories])

    async function createCategory(request: CreateCategoryRequest): Promise<void> {
        const response = await apiClient.post<CategoryDto>('/categories', request)
        setCategories((previous) => [...previous, response.data])
    }

    async function deactivateCategory(id: string): Promise<void> {
        await apiClient.delete(`/categories/${id}`)
        setCategories((previous) => previous.filter((category) => category.id !== id))
    }

    return { categories, isLoading, error, createCategory, deactivateCategory, refreshCategories: fetchCategories }
}
