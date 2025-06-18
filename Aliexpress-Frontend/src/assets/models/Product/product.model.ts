export interface ProductDto {
    id: number;
    name: string;
    description?: string;
    price: number;
    discount?: number;
    discountedPrice?: number;
    stockQuantity: number;
    categoryId: number;
    categoryName: string;
    sellerId: number;
    sellerName: string;
    imageUrls: string[];
    rating?: number;
    isActive: boolean;
    createdDate: string;
    updatedDate?: string; 
}