export interface ProductFilterDto {
    searchTerm?: string;
    categoryId?: number;
    minPrice?: string;
    maxPrice?: string;
    sellerId?: number;
    inStock?: boolean;
    hasDiscount?: boolean;
    minRating?: number;
    sortBy?: string;
    sortAscending?: boolean;
    pageNumber?: number;
    pageSize?: number;
}