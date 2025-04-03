import { Component } from '@angular/core';
import { TopProductsComponent } from '../top-products/top-products.component';
import { DiscountsComponent } from '../discounts/discounts.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [TopProductsComponent, DiscountsComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent { }
