import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-track',
  standalone: true,
  imports: [],
  templateUrl: './track.component.html',
  styleUrls: ['./track.component.scss']
})
export class TrackComponent {
  constructor(private router: Router) {}

  goBack(): void {
    this.router.navigate(['/profile/orders/orders-tab']);
  }
}