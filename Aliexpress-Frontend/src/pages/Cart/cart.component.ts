import { Component } from '@angular/core';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent {
  cartItems = [
    { name: 'Product 1', price: 100, quantity: 1, image: 'assets/product1.jpg' },
    { name: 'Product 2', price: 200, quantity: 2, image: 'assets/product2.jpg' },
    { name: 'Product 3', price: 150, quantity: 1, image: 'assets/product3.jpg' }
  ];

  getTotal() {
    return this.cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0);
  }

  removeItem(item: any) {
    this.cartItems = this.cartItems.filter(cartItem => cartItem !== item);
  }

  checkout() {
    console.log('Proceeding to checkout');
    // Логика оформления заказа
  }
}
