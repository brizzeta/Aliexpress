import { Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet, RouterModule, Router, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs/operators';
import { HeaderComponent } from './Header/header.component';
import { FooterComponent } from './Footer/footer.component';
import { HomeComponent } from './Home/home.component';
import { LoginComponent } from './Login/login.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ 
    RouterOutlet, 
    RouterModule, 
    HeaderComponent, 
    FooterComponent, 
    HomeComponent, 
    LoginComponent, 
    CommonModule
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent { 
  title = 'Aliexpress-Front';
  currentUrl: string = '';

  constructor(private router: Router) {
    this.router.events.pipe( // Подписываемся на события навигации
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      this.currentUrl = event.urlAfterRedirects;
    });
  }
  shouldHideHeaderFooter(): boolean { // Список URL-адресов, где хедер и футер должны быть скрыты
    const routesWithoutHeaderFooter = [
      '/admin',
      '/profile/login',
    ];
    return routesWithoutHeaderFooter.some(route => this.currentUrl.includes(route));
  }
}