import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { Router, RouterModule, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-coupons',
  standalone: true,
  imports: [CommonModule, RouterModule], // Добавляем RouterModule для router-outlet
  templateUrl: './coupons.component.html',
  styleUrls: ['./coupons.component.scss']
})
export class CouponsComponent implements OnInit {
  private platformId = inject(PLATFORM_ID);
  public showCoupons: boolean = true;
  public isNoactiveRoute: boolean = false;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.checkRoute();

    this.router.events
      .subscribe((event) => {
        if (event instanceof NavigationEnd) {
          this.checkRoute();
        }
      });
  }

  private checkRoute(): void {
    const url = this.router.url;
    this.showCoupons = !url.includes('noactive');
    this.isNoactiveRoute = url.includes('noactive');
  }

  isMobile(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return window.innerWidth <= 768;
    }
    return false;
  }

  goBack(): void {
    this.router.navigate(['/profile']);
  }

  onOutletActivated(component: any) {
    this.isNoactiveRoute = !!component;
    this.showCoupons = !this.isNoactiveRoute;
  }
}