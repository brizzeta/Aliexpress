import { Component, OnInit, HostListener, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  private breakpointSm = 768;
  public isMenuVisible: boolean = true;

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {}

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    if (isPlatformBrowser(this.platformId)) {
      this.checkMenuVisibility();
    }
  }

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.checkMenuVisibility();
    }
  }

  public checkMenuVisibility(): void {
    if (isPlatformBrowser(this.platformId)) {
      const isMobile = window.innerWidth < this.breakpointSm;
      this.isMenuVisible = !isMobile || (isMobile && this.isMenuVisible);
    }
  }

  public showContent(): void {
    if (isPlatformBrowser(this.platformId) && window.innerWidth < this.breakpointSm) {
      this.isMenuVisible = false;
    }
  }

  public showMenu(): void {
    this.isMenuVisible = true;
  }
}