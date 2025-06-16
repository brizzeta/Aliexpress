import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-seller-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class SellerOrdersComponent {
  constructor(private router: Router) {}

  navigateToProfile() {
    this.router.navigate(['/profile/seller-profile']);
  }
}