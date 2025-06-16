import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { Router, RouterModule, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-addresses',
  standalone: true,
  imports: [CommonModule, RouterModule], // Добавляем RouterModule для router-outlet
  templateUrl: './addresses.component.html',
  styleUrls: ['./addresses.component.scss']
})
export class AddressesComponent implements OnInit {
  private platformId = inject(PLATFORM_ID);
  public showAddresses: boolean = true;
  public isNoactiveRoute: boolean = false;
  isSelected: { [key: number]: boolean } = { 1: false, 2: false };

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
    this.showAddresses = !url.includes('noactive');
    this.isNoactiveRoute = url.includes('noactive');
  }

  goBackToProfile() {
    this.router.navigate(['/profile']);
  }

  toggleSelection(event: Event, index: number) {
    event.stopPropagation();
    this.isSelected[index] = !this.isSelected[index];
  }

  goToNewAddress() {
    this.router.navigate(['/profile/addresses/newaddress']);
  }

  goToEditAddress(id: number) {
    this.router.navigate(['/profile/addresses/createaddress', id]);
  }

  onOutletActivated(component: any) {
    this.isNoactiveRoute = !!component;
    this.showAddresses = !this.isNoactiveRoute;
  }
}