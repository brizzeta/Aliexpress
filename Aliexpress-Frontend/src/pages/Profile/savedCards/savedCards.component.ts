import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { Router, RouterModule, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { isPlatformBrowser } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-saved-cards',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule], // Добавляем RouterModule для router-outlet
  templateUrl: './savedCards.component.html',
  styleUrls: ['./savedCards.component.scss']
})
export class SavedCardsComponent implements OnInit {
  private platformId = inject(PLATFORM_ID);
  isPopupOpen = false;
  popupMode: 'edit' | 'new' = 'new';
  selectedCardIndex: number | null = null;
  public showSavedCards: boolean = true;
  public isNoactiveRoute: boolean = false;

  months = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];
  years = Array.from({ length: 11 }, (_, i) => (2025 + i).toString());

  cards = [
    { type: 'master-card', number: '...9298', month: '04', year: '2028' },
    { type: 'visa', number: '...4512', month: '06', year: '2027' },
    { type: 'master-card', number: '...7846', month: '09', year: '2029' }
  ];

  newCard = {
    type: 'visa',
    number: '4441 1110 3400 6043',
    month: '',
    year: '',
    cvv: ''
  };

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
    this.showSavedCards = !url.includes('noactive');
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

  openPopup(mode: 'edit' | 'new', index?: number): void {
    this.popupMode = mode;
    if (mode === 'edit' && index !== undefined) {
      this.selectedCardIndex = index;
    } else {
      this.newCard = { type: 'visa', number: '4441 1110 3400 6043', month: '', year: '', cvv: '' };
    }
    this.isPopupOpen = true;
  }

  closePopup(): void {
    this.isPopupOpen = false;
    this.selectedCardIndex = null;
  }

  saveCard(): void {
    this.closePopup();
  }

  saveNewCard(): void {
    if (this.newCard.month && this.newCard.year && this.newCard.cvv) {
      this.cards.push({ ...this.newCard, number: '...XXXX' });
      this.closePopup();
    } else {
      alert('Please fill all fields');
    }
  }

  onOutletActivated(component: any) {
    this.isNoactiveRoute = !!component;
    this.showSavedCards = !this.isNoactiveRoute;
  }
}