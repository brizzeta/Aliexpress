import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { Router, RouterModule, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [CommonModule, RouterModule], // Добавляем RouterModule для router-outlet
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {
  private platformId = inject(PLATFORM_ID);
  public showHistory: boolean = true;
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
    this.showHistory = !url.includes('noactive');
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
    this.showHistory = !this.isNoactiveRoute;
  }
}