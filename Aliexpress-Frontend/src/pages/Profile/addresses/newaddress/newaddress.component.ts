import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-newaddress',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './newaddress.component.html',
  styleUrls: ['./newaddress.component.scss']
})
export class NewaddressComponent implements OnInit {
  private platformId = inject(PLATFORM_ID);

  constructor(private router: Router) {}

  ngOnInit(): void {}

  isMobile(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return window.innerWidth <= 768;
    }
    return false;
  }

  goBack(): void {
    this.router.navigate(['/profile/addresses']);
  }

  createAddress(): void {
    // Логика создания нового адреса (пока заглушка)
    console.log('Creating new delivery address');
    this.router.navigate(['/profile/addresses']);
  }
}