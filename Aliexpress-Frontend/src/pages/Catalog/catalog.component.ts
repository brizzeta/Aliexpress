import { Component } from '@angular/core';
import { ProductCardComponent } from '../product-card/product-card.component';

@Component({
  selector: 'app-catalog',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss']
})
export class CatalogComponent {
  products = [
    { name: 'Product 1', description: 'Description of product 1', price: 100, image: 'assets/product1.jpg' },
    { name: 'Product 2', description: 'Description of product 2', price: 200, image: 'assets/product2.jpg' },
    { name: 'Product 3', description: 'Description of product 3', price: 150, image: 'assets/product3.jpg' }
  ];
}
