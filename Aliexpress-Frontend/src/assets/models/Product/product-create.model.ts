export interface ProductCreateDto {
    name: string;
    description?: string;
    price: number;
    discount?: number;
    stockQuantity: number;
    categoryId: number;
    sellerId: number;
    imageUrls: string[];
    isActive: boolean;
}