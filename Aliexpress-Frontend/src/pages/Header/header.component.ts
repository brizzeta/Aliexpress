import { Component, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  isCatalogActive = false;
  isLangActive = false;
  isMenuActive = false;
  isCatalogFadingOut = false;
  isLangFadingOut = false;
  isMenuFadingOut = false;

  @ViewChild('catalogContainer') catalogContainer!: ElementRef;
  @ViewChild('langContainer') langContainer!: ElementRef;
  @ViewChild('menuContainer') menuContainer!: ElementRef;

  closeOtherContainers(except: string): void { // Универсальный метод для закрытия других контейнеров
    if (except !== 'catalog' && this.isCatalogActive) {
      this.isCatalogFadingOut = true;
      setTimeout(() => {
        this.isCatalogActive = false;
        this.isCatalogFadingOut = false;
      }, 400);
    }
    if (except !== 'lang' && this.isLangActive) {
      this.isLangFadingOut = true;
      setTimeout(() => {
        this.isLangActive = false;
        this.isLangFadingOut = false;
      }, 400);
    }
    if (except !== 'menu' && this.isMenuActive) {
      this.isMenuFadingOut = true;
      setTimeout(() => {
        this.isMenuActive = false;
        this.isMenuFadingOut = false;
      }, 400);
    }
  }

  toggleLanguageDropdown(): void {
    this.closeOtherContainers('lang');
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
    this.closeOtherContainers('menu');
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

  toggleCatalogDropdown(): void {
    this.closeOtherContainers('catalog');
    if (this.isCatalogActive) {
      this.isCatalogFadingOut = true;
      setTimeout(() => {
        this.isCatalogActive = false;
        this.isCatalogFadingOut = false;
      }, 400);
    } else {
      this.isCatalogActive = true;
      this.isCatalogFadingOut = false;
    }
  }
}