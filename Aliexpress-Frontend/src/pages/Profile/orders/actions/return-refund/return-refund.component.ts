import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-return-refund',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './return-refund.component.html',
  styleUrls: ['./return-refund.component.scss']
})
export class ReturnRefundComponent {
  constructor(private router: Router, private route: ActivatedRoute) {}

  goBack(): void {
    const parentUrl = this.route.snapshot.parent?.url.map(segment => segment.path).join('/');
    if (parentUrl?.includes('delivered')) {
      this.router.navigate(['/profile/orders/delivered']);
    } else if (parentUrl?.includes('orders-tab')) {
      this.router.navigate(['/profile/orders/orders-tab']);
    } else {
      this.router.navigate(['/profile/orders']);
    }
  }
}