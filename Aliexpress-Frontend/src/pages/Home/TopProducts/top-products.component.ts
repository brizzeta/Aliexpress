import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-top-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './top-products.component.html',
  styleUrls: ['./top-products.component.scss']
})
export class TopProductsComponent {
  filters = [
    'All', 
    'Home and comfort', 
    'Kitchen', 
    'Beauty and care', 
    'Goods for children', 
    'Office',
  ];

  activeFilter: string = ''; // Переменная для хранения активного фильтра

  setActiveFilter(filter: string): void {
    this.activeFilter = this.activeFilter === filter ? '' : filter; // Логика выбора фильтра
  }
}


