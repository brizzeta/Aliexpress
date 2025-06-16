import { Component, OnInit } from '@angular/core';
import { Router, RouterModule, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-seller',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './seller.component.html',
  styleUrls: ['./seller.component.scss']
})
export class SellerComponent implements OnInit {
  activeTab: string = 'products_all'; // По умолчанию выбрана "All products"

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.checkActiveTab();

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.checkActiveTab();
      }
    });
  }

  checkActiveTab(): void {
    const url = this.router.url;
    if (url.endsWith('products_add')) {
      this.activeTab = 'products_add';
    } else if (url.endsWith('orders')) {
      this.activeTab = 'orders';
    } else {
      this.activeTab = 'products_all'; // Дефолтный выбор
    }
  }
}