import { OrderStatus } from "../../enums/order-status.enum"

export interface OrderStatusUpdateDto {
    status: OrderStatus;
}