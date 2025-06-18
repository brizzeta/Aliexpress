import { OrderStatus } from "../../enums/order-status.enum";

export interface OrderUpdateDto {
    status: OrderStatus;
    shippingAddress?: string;
}