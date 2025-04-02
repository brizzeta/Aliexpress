import { Component } from '@angular/core';
import { ProductCardComponent } from '../product-card/product-card.component';

@Component({
  selector: 'app-top-products',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './top-products.component.html',
  styleUrls: ['./top-products.component.scss']
})
export class TopProductsComponent {
  products = [
    { name: 'Top Product 1', description: 'Description of product 1', price: 100, image: 'assets/product1.jpg' },
    { name: 'Top Product 2', description: 'Description of product 2', price: 200, image: 'assets/product2.jpg' },
    { name: 'Top Product 3', description: 'Description of product 3', price: 150, image: 'assets/product3.jpg' }
  ];
}
