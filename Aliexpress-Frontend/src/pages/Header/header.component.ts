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
  isFadingOut = false;

  @ViewChild('langContainer') langContainer!: ElementRef;

  toggleLanguageDropdown(): void {
    if (this.isLangActive) {
      this.isFadingOut = true;

      setTimeout(() => {
        this.isLangActive = false;
        this.isFadingOut = false;
      }, 400);
    } else {
      this.isLangActive = true;
      this.isFadingOut = false;
    }
  }
}