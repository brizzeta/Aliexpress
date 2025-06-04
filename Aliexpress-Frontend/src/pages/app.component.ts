import { Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet, RouterModule, Router, NavigationEnd, Event as RouterEvent } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs/operators';

import { HeaderComponent } from './Header/header.component';
import { FooterComponent } from './Footer/footer.component';
import { HomeComponent } from './Home/home.component';
import { LoginComponent } from './Login/login.component';
import { TopProductsComponent } from './Home/TopProducts/top-products.component';
import { DiscountsComponent } from './Home/Discounts/discounts.component';
import { ProductCardComponent } from './Home/ProductCard/product-card.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterModule,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    TopProductsComponent,
    DiscountsComponent,
    ProductCardComponent,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent {
  title = 'Aliexpress-Front';
  currentUrl: string = '';
  showMainContent: boolean = true;
  showFooter: boolean = true;

  constructor(private router: Router) {
    this.router.events
      .pipe(filter((event: RouterEvent): event is NavigationEnd => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.currentUrl = event.urlAfterRedirects;
        this.showMainContent = !this.currentUrl.startsWith('/profile');
        const routesWithoutFooter = ['/admin', '/profile/login'];
        this.showFooter = !routesWithoutFooter.some(route => this.currentUrl.includes(route));
      });
  }

  shouldHideHeaderFooter(): boolean {
    const routesWithoutHeaderFooter = ['/admin', '/profile/login'];
    return routesWithoutHeaderFooter.some(route => this.currentUrl.includes(route));
  }
}