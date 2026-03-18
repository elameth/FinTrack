import { type FormEvent, useState } from 'react'
import axios from 'axios'
import Modal from './Modal.tsx'
import type { CreateCategoryRequest } from '../types/category.ts'

interface CreateCategoryModalProperties {
    isOpen: boolean
    onClose: () => void
    onCreateCategory: (request: CreateCategoryRequest) => Promise<void>
}

export default function CreateCategoryModal({
    isOpen,
    onClose,
    onCreateCategory,
}: CreateCategoryModalProperties) {
    const [name, setName] = useState('')
    const [description, setDescription] = useState('')
    const [icon, setIcon] = useState('')
    const [color, setColor] = useState('#10b981')
    const [error, setError] = useState('')
    const [isLoading, setIsLoading] = useState(false)

    function resetForm() {
        setName('')
        setDescription('')
        setIcon('')
        setColor('#10b981')
        setError('')
    }

    function handleClose() {
        resetForm()
        onClose()
    }

    async function handleSubmit(event: FormEvent) {
        event.preventDefault()
        setError('')
        setIsLoading(true)

        try {
            await onCreateCategory({
                name,
                description: description || undefined,
                icon: icon || undefined,
                color: color || undefined,
            })
            resetForm()
            onClose()
        } catch (err) {
            if (axios.isAxiosError(err)) {
                const data = err.response?.data
                if (data?.errors) {
                    const messages = Object.values(data.errors).flat()
                    setError(messages.join('\n'))
                } else {
                    setError(data?.detail ?? 'Failed to create category.')
                }
            } else {
                setError('Something went wrong. Please try again.')
            }
        } finally {
            setIsLoading(false)
        }
    }

    return (
        <Modal isOpen={isOpen} onClose={handleClose} title="New Category">
            {error && (
                <div className="bg-red-900/30 text-red-400 text-sm rounded-md p-3 mb-4 whitespace-pre-line">
                    {error}
                </div>
            )}

            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label htmlFor="categoryName" className="block text-sm font-medium text-gray-300 mb-1">
                        Name
                    </label>
                    <input
                        id="categoryName"
                        type="text"
                        required
                        maxLength={100}
                        value={name}
                        onChange={(event) => setName(event.target.value)}
                        className="w-full px-3 py-2 bg-gray-700 text-gray-100 border border-gray-600 rounded-md
                                   focus:outline-none focus:ring-2 focus:ring-emerald-500 focus:border-transparent
                                   placeholder-gray-400"
                        placeholder="e.g. Groceries"
                    />
                </div>

                <div>
                    <label htmlFor="categoryDescription" className="block text-sm font-medium text-gray-300 mb-1">
                        Description
                    </label>
                    <textarea
                        id="categoryDescription"
                        value={description}
                        onChange={(event) => setDescription(event.target.value)}
                        rows={3}
                        className="w-full px-3 py-2 bg-gray-700 text-gray-100 border border-gray-600 rounded-md
                                   focus:outline-none focus:ring-2 focus:ring-emerald-500 focus:border-transparent
                                   placeholder-gray-400 resize-none"
                        placeholder="Optional description"
                    />
                </div>

                <div>
                    <label htmlFor="categoryIcon" className="block text-sm font-medium text-gray-300 mb-1">
                        Icon
                    </label>
                    <input
                        id="categoryIcon"
                        type="text"
                        value={icon}
                        onChange={(event) => setIcon(event.target.value)}
                        className="w-full px-3 py-2 bg-gray-700 text-gray-100 border border-gray-600 rounded-md
                                   focus:outline-none focus:ring-2 focus:ring-emerald-500 focus:border-transparent
                                   placeholder-gray-400"
                        placeholder="Optional icon name"
                    />
                </div>

                <div>
                    <label htmlFor="categoryColor" className="block text-sm font-medium text-gray-300 mb-1">
                        Color
                    </label>
                    <input
                        id="categoryColor"
                        type="color"
                        value={color}
                        onChange={(event) => setColor(event.target.value)}
                        className="w-16 h-10 bg-gray-700 border border-gray-600 rounded-md cursor-pointer"
                    />
                </div>

                <div className="flex justify-end gap-3 pt-2">
                    <button
                        type="button"
                        onClick={handleClose}
                        className="px-4 py-2 text-gray-300 bg-gray-700 rounded-md hover:bg-gray-600
                                   transition-colors"
                    >
                        Cancel
                    </button>
                    <button
                        type="submit"
                        disabled={isLoading}
                        className="px-4 py-2 bg-emerald-600 text-white font-medium rounded-md
                                   hover:bg-emerald-700 disabled:opacity-50 disabled:cursor-not-allowed
                                   transition-colors"
                    >
                        {isLoading ? 'Creating...' : 'Create Category'}
                    </button>
                </div>
            </form>
        </Modal>
    )
}
