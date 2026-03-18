export interface BudgetDto {
    id: string
    name: string
    categoryId: string
    userId: string
    amount: number
    currency: number
    periodStartDate: string
    periodEndDate: string
    spentAmount: number
    spentPercentage: number
    createdAt: string
    updatedAt: string | null
}

export interface CreateBudgetRequest {
    name: string
    categoryId: string
    amount: number
    currency: number
    periodStartDate: string
    periodEndDate: string
}
