import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { Router, RouterModule, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, RouterModule], // Добавляем RouterModule для router-outlet
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  private platformId = inject(PLATFORM_ID);
  selectedChat = 'KlikAVASupport';
  showLeftPanel = true; // Изначально показываем только left-panel
  public showChat: boolean = true;
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
    this.showChat = !url.includes('noactive');
    this.isNoactiveRoute = url.includes('noactive');
  }

  isMobile(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return window.innerWidth <= 768;
    }
    return false;
  }

  goBack(): void {
    if (this.isMobile() && !this.showLeftPanel) {
      // Если на мобильном и показан right-panel, возвращаемся к left-panel
      this.showLeftPanel = true;
    } else {
      // Иначе возвращаемся к профилю
      this.router.navigate(['/profile']);
    }
  }

  onChatSelect(chat: string) {
    this.selectedChat = chat;
    if (this.isMobile()) {
      // На мобильном устройстве скрываем left-panel и показываем right-panel
      this.showLeftPanel = false;
    }
  }

  onOutletActivated(component: any) {
    this.isNoactiveRoute = !!component;
    this.showChat = !this.isNoactiveRoute;
  }
}