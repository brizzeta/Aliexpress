import { Component, HostListener, Inject, PLATFORM_ID, OnInit } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { RouterModule, Router, NavigationEnd, Event as RouterEvent } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from '../profile.component';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  private breakpointSm = 768;
  public showTabs: boolean = true; // Добавляем переменную для управления отображением .tabs

  constructor(
    private profileComponent: ProfileComponent,
    @Inject(PLATFORM_ID) private platformId: Object,
    private router: Router // Добавляем Router
  ) {}

  ngOnInit(): void {
    this.checkRoute(); // Проверяем маршрут при инициализации

    // Подписываемся на события навигации
    this.router.events
      .pipe(
        filter((event: RouterEvent): event is NavigationEnd => event instanceof NavigationEnd)
      )
      .subscribe((event: NavigationEnd) => {
        this.checkRoute();
      });
  }

  private checkRoute(): void {
    const url = this.router.url;
    // Скрываем .tabs, если маршрут содержит track, review, return-refund или buy-again
    this.showTabs = !url.includes('track') && 
                   !url.includes('review') && 
                   !url.includes('return-refund') && 
                   !url.includes('buy-again');
  }

  public isMobile(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return window.innerWidth < this.breakpointSm;
    }
    return false;
  }

  public goBack(): void {
    this.profileComponent.showMenu();
  }

  @HostListener('window:resize', ['$event'])
  public onResize(event: Event): void {
    this.isMobile();
  }
}