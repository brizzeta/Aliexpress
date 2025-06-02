import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent {
  constructor(private router: Router, private route: ActivatedRoute) {}

  goBack(): void {
    const parentUrl = this.route.snapshot.parent?.url.map(segment => segment.path).join('/');
    if (parentUrl?.includes('delivered')) {
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