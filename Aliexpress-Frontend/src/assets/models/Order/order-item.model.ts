export interface OrderItemDto {
    id: number;
    orderId: number;
    productId: number;
    productName: string;
    quantity: number;
    pricePerItem: number;
    subtotal: number;
}