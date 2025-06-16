export interface ProductSimpleDto {
    id: number;
    name: string;
    price: number;
    discount?: number;
    imageUrl?: string;
    sellerId: number;
    sellerName: string;
}