import { ProductDto } from './product.model';
import { ProductReviewDto } from './product-review.model';

export interface ProductDetailDto extends ProductDto {
    reviews: ProductReviewDto[];
    sellerEmail?: string;
    sellerPhone?: string;
    sellerRating: number;
}