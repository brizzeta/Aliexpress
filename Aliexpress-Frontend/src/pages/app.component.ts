import { Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { HeaderComponent } from './Header/header.component';
import { FooterComponent } from './Footer/footer.component';
import { HomeComponent } from './Home/home.component';
import { TopProductsComponent } from './Home/TopProducts/top-products.component';
import { DiscountsComponent } from './Home/Discounts/discounts.component'; 
import { ProductCardComponent } from './Home/ProductCard/product-card.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    TopProductsComponent,
    DiscountsComponent,
    ProductCardComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent {
  title = 'Aliexpress-Front';
}