import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-saved-cards', // Оставляем селектор с дефисом, так как это стандарт для HTML
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './savedCards.component.html',
  styleUrls: ['./savedCards.component.scss']
})
export class SavedCardsComponent {}