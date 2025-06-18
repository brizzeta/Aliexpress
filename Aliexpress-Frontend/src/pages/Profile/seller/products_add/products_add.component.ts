import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-add',
  standalone: true,
  templateUrl: './products_add.component.html', // Исправлен путь
  styleUrls: ['./products_add.component.scss']
})
export class ProductsAddComponent {
  constructor(private router: Router) {}

  navigateToProfile() {
    this.router.navigate(['/profile/seller-profile']);
  }
}