import { OrderStatus } from "../../enums/order-status.enum";
import { OrderItemDto } from "./order-item.model";
import { PaymentDto } from "./payment.model";

export interface OrderDto {
    id: number;
    buyerId: number;
    buyerName: string;
    productId: number;
    productName: string;
    quantity: number;
    totalPrice: number;
    status: OrderStatus;
    createdAt: string;
    updatedAt?: string;
    shippingAddress?: string;
    orderItems: OrderItemDto[];
    payments: PaymentDto[];
}