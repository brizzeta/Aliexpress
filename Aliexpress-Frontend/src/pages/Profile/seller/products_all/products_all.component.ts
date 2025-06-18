import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

// Интерфейс для состояния одного выпадающего списка
interface DropdownState {
  isOpen: boolean;
  selected: string;
}

// Интерфейс для объекта dropdownStates
interface DropdownStates {
  [key: string]: DropdownState; // Индексная сигнатура
  stock: DropdownState;
  price: DropdownState;
  date: DropdownState;
  tags: DropdownState;
  categories: DropdownState;
}

@Component({
  selector: 'app-product-all',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products_all.component.html',
  styleUrls: ['./products_all.component.scss']
})
export class ProductsAllComponent {
  isPopupVisible = false;
  dropdownStates: DropdownStates = {
    stock: { isOpen: false, selected: 'Select' },
    price: { isOpen: false, selected: 'Select' },
    date: { isOpen: false, selected: 'Select' },
    tags: { isOpen: false, selected: 'Select' },
    categories: { isOpen: false, selected: 'Select' }
  };

  constructor(private router: Router) {}

  navigateToProfile() {
    this.router.navigate(['/profile/seller-profile']);
  }

  togglePopup() {
    this.isPopupVisible = !this.isPopupVisible;
    if (!this.isPopupVisible) {
      this.closeAllDropdowns();
    }
  }

  toggleDropdown(field: keyof DropdownStates) {
    Object.keys(this.dropdownStates).forEach((key: string) => {
      if (key !== field) {
        this.dropdownStates[key].isOpen = false;
      }
    });
    this.dropdownStates[field].isOpen = !this.dropdownStates[field].isOpen;
  }

  closeAllDropdowns() {
    Object.keys(this.dropdownStates).forEach((key: string) => {
      this.dropdownStates[key].isOpen = false;
    });
  }

  selectOption(field: keyof DropdownStates, option: string) {
    this.dropdownStates[field].selected = option;
    this.dropdownStates[field].isOpen = false;
  }
}