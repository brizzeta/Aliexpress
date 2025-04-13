import { Component, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  isLangActive = false;
  isMenuActive = false;
  isLangFadingOut = false;
  isMenuFadingOut = false;

  @ViewChild('langContainer') langContainer!: ElementRef;
  @ViewChild('menuContainer') menuContainer!: ElementRef;

  toggleLanguageDropdown(): void {
    if (this.isLangActive) {
      this.isLangFadingOut = true;

      setTimeout(() => {
        this.isLangActive = false;
        this.isLangFadingOut = false;
      }, 400);
    } else {
      this.isLangActive = true;
      this.isLangFadingOut = false;
    }
  }
  toggleMenuDropdown(): void {
    if (this.isMenuActive) {
      this.isMenuFadingOut = true;

      setTimeout(() => {
        this.isMenuActive = false;
        this.isMenuFadingOut = false;
      }, 400);
    } else {
      this.isMenuActive = true;
      this.isMenuFadingOut = false;
    }
  }
}