import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [],
  templateUrl: './404.component.html',
  styleUrls: ['./404.component.scss']
})
export class NotFoundComponent {

  constructor(private router: Router) { }

  goHome() {
    this.router.navigate(['/']); // Перенаправляет на главную страницу
  }
}
