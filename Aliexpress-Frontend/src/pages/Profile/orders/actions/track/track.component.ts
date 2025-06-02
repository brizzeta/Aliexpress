import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-track',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './track.component.html',
  styleUrls: ['./track.component.scss']
})
export class TrackComponent {
  constructor(private router: Router, private route: ActivatedRoute) {}

  goBack(): void {
    const parentUrl = this.route.snapshot.parent?.url.map(segment => segment.path).join('/');
    if (parentUrl?.includes('processing')) {
      this.router.navigate(['/profile/orders/processing']);
    } else if (parentUrl?.includes('sent')) {
      this.router.navigate(['/profile/orders/sent']);
    } else if (parentUrl?.includes('delivered')) {
      this.router.navigate(['/profile/orders/delivered']);
    } else if (parentUrl?.includes('return')) {
      this.router.navigate(['/profile/orders/return']);
    } else if (parentUrl?.includes('orders-tab')) {
      this.router.navigate(['/profile/orders/orders-tab']);
    } else {
      this.router.navigate(['/profile/orders']);
    }
  }
}