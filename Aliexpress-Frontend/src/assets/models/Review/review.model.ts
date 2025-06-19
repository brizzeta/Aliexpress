export interface Review {
    id: number;
    buyerId: number;
    buyerName: string;
    productId: number;
    productName: string;
    sellerId: number;
    sellerName: string;
    rating: number;
    comment?: string;
    createdDate: Date;
  }