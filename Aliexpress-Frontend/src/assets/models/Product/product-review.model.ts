export interface ProductReviewDto {
    id: number;
    buyerId: number;
    buyerName: string;
    rating: number;
    comment?: string;
    createdDate: string;
}