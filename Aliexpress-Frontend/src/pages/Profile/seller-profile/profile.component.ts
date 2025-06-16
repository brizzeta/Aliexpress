import { Component } from '@angular/core';
import { Router, NavigationEnd, RouterModule } from '@angular/router';

@Component({
  selector: 'app-seller-profile',
  standalone: true,
  imports: [RouterModule], // Добавим RouterModule для router-outlet
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class SellerProfileComponent {
  activeTab: string = 'my-profile';

  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateActiveTab();
      }
    });
  }

  updateActiveTab() {
    const url = this.router.url;
    if (url.includes('my-profile')) this.activeTab = 'my-profile';
    else if (url.includes('account-settings')) this.activeTab = 'account-settings';
    else if (url.includes('statistic')) this.activeTab = 'statistic';
    else if (url.includes('dashboard')) this.activeTab = 'dashboard';
  }
}