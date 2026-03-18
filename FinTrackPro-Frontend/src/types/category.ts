export interface CategoryDto {
    id: string
    name: string
    description: string | null
    icon: string | null
    color: string | null
    isActive: boolean
    createdAt: string
    updatedAt: string | null
}

export interface CreateCategoryRequest {
    name: string
    description?: string
    icon?: string
    color?: string
}
