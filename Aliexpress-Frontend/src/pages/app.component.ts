import { Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common'; // Добавляем CommonModule
import { HeaderComponent } from './Header/header.component';
import { FooterComponent } from './Footer/footer.component';
import { TopProductsComponent } from './Home/TopProducts/top-products.component';
import { DiscountsComponent } from './Home/Discounts/discounts.component'; 
import { ProductCardComponent } from './Home/ProductCard/product-card.component';
import { Router, NavigationEnd, Event as RouterEvent } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, // Добавляем сюда
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
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
  showMainContent: boolean = true;

  constructor(private router: Router) {
    this.showMainContent = !this.router.url.startsWith('/profile');

    this.router.events
      .pipe(
        filter((event: RouterEvent): event is NavigationEnd => event instanceof NavigationEnd)
      )
      .subscribe((event: NavigationEnd) => {
        this.showMainContent = !event.urlAfterRedirects.startsWith('/profile');
      });
  }
}