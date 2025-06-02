import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-createaddress',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './createaddress.component.html',
  styleUrls: ['./createaddress.component.scss']
})
export class CreateaddressComponent implements OnInit {
  private platformId = inject(PLATFORM_ID);
  addressId: number | null = null;

  // Заглушка для данных адреса
  private addressData: { [key: number]: { [key: string]: string } } = {
    1: {
      firstName: 'Evgeniy',
      lastName: 'Timofeev',
      country: 'Ukraine',
      phone: '+380 66 901 12 05',
      address: 'с. Odesa, s/p. Odessa UA 65007',
      postalCode: '65007',
      city: 'Ismail 54'
    },
    2: {
      firstName: 'Alexey',
      lastName: 'Ivanov',
      country: 'Ukraine',
      phone: '+380 97 123 45 67',
      address: 'с. Kyiv, s/p. Kyiv UA 01001',
      postalCode: '01001',
      city: 'Kyiv 12'
    }
  };

  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.addressId = +params['id'] || null;
    });
  }

  isMobile(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return window.innerWidth <= 768;
    }
    return false;
  }

  goBack(): void {
    this.router.navigate(['/profile/addresses']);
  }

  getAddressField(field: string): string {
    if (this.addressId && this.addressData[this.addressId]) {
      return this.addressData[this.addressId][field] || '';
    }
    return '';
  }

  deleteAddress(): void {
    // Логика удаления адреса (пока заглушка)
    console.log(`Deleting address with ID: ${this.addressId}`);
    this.router.navigate(['/profile/addresses']);
  }

  saveAddress(): void {
    // Логика сохранения адреса (пока заглушка)
    console.log(`Saving address with ID: ${this.addressId}`);
    this.router.navigate(['/profile/addresses']);
  }
}