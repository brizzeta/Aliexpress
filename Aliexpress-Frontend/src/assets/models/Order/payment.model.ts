import { PaymentStatus } from "../../enums/payment-status.enum";
import { PaymentMethod } from "../../enums/payment-method.enum";

export interface PaymentDto {
    id: number;
    orderId: number;
    paymentDate: string;
    status: PaymentStatus;
    paymentMentod: PaymentMethod;
    transactionId?: string;
}