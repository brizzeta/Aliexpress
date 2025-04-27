import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent {
  constructor(private router: Router) {}

  goBack(): void {
    this.router.navigate(['/profile/orders']);
  }
}