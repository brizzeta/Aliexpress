import { PaymentMethod } from "../../enums/payment-method.enum";

export interface PaymentCreateDto {
    orderId: number;
    paymentMethod: PaymentMethod;
    transactionId?: string;
}