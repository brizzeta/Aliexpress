export interface ProductUpdateDto {
    name?: string;
    description?: number;
    price?: number;
    discount?: number;
    stockQuantity?: number;
    categoryId?: number;
    imageUrls?: string[];
    isActive?: boolean;
}