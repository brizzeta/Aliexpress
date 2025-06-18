import { OrderDto } from "./order.model";
import { ProductSimpleDto } from "./product-simple.model";

export interface OrderDeatilDto extends OrderDto {
    buyerEmail: string;
    buyerPhone?: string;
    product: ProductSimpleDto;
}