import { OrderItemCreateDto } from "./order-item-create.model";

export interface OrderCreateDto {
    productId: number;
    quantity: number;
    shippingAddress?: string;
    orderItems: OrderItemCreateDto[];
}