export interface ReviewCreate {
    productId: number;
    rating: number;
    comment?: string;
  }