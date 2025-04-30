import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface User {
  firstName: string;
  lastName: string;
  email: string;
  role: string;
}

@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.scss'
})
export class AdminUsersComponent implements OnInit {
  users: User[] = [];   // Массив пользователей
  // Для фильтрации
  globalSearchText: string = '';
  searchText: string = '';
  
  ngOnInit(): void { // Инициализация массива пользователей
    this.users = [
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'super admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'seller' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'customer' }
    ];
  }
  
  get filteredUsers(): User[] { // Фильтрация пользователей
    const globalFilter = this.globalSearchText.toLowerCase();
    const specificFilter = this.searchText.toLowerCase();
    
    return this.users.filter(user => {
      const matchesGlobal = !globalFilter || // Проверяем глобальный поиск
        user.firstName.toLowerCase().includes(globalFilter) ||
        user.lastName.toLowerCase().includes(globalFilter) ||
        user.email.toLowerCase().includes(globalFilter) ||
        user.role.toLowerCase().includes(globalFilter);
      const matchesSpecific = !specificFilter || // Проверяем конкретный поиск
        user.firstName.toLowerCase().includes(specificFilter) ||
        user.lastName.toLowerCase().includes(specificFilter) ||
        user.email.toLowerCase().includes(specificFilter) ||
        user.role.toLowerCase().includes(specificFilter);
      return matchesGlobal && matchesSpecific;
    });
  }
}