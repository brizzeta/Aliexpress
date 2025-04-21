import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // Добавляем CommonModule

@Component({
  selector: 'app-orders',
  standalone: true, // Делаем компонент standalone
  imports: [
    CommonModule // Импортируем CommonModule для *ngIf
  ],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent {
  activeTab: string = 'all'; // По умолчанию активна вкладка "All Orders"

  // Метод для смены активной вкладки
  setActiveTab(tab: string) {
    this.activeTab = tab;
  }
}