import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Admin {
  firstName: string;
  lastName: string;
  email: string;
  role: string;
  selected?: boolean;
}
@Component({
  selector: 'app-admin-admins',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-admins.component.html',
  styleUrl: './admin-admins.component.scss'
})
export class AdminAdminsComponent implements OnInit {
  admins: Admin[] = []; // Массив админов
  // Для фильтрации
  globalSearchText: string = '';
  searchText: string = '';
  // Для пагинации
  currentPage: number = 1;
  pageSize: number = 5;
  pageSizeOptions: number[] = [1, 3, 5];
  totalPages: number = 1;
  allSelected: boolean = false; // Выбор чекбоксов

  ngOnInit(): void { // Инициализация массива админов
    this.admins = [
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'super admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'admin' },
    ];
    document.body.style.overflow = 'hidden';
    this.updateTotalPages();
  }
  get filteredAdmins(): Admin[] { // Фильтрация админов
    const globalFilter = this.globalSearchText.toLowerCase();
    const specificFilter = this.searchText.toLowerCase();
    const filtered = this.admins.filter(admin => {
      const matchesGlobal = !globalFilter || // Проверяем глобальный поиск
        admin.firstName.toLowerCase().includes(globalFilter) ||
        admin.lastName.toLowerCase().includes(globalFilter) ||
        admin.email.toLowerCase().includes(globalFilter) ||
        admin.role.toLowerCase().includes(globalFilter);
      const matchesSpecific = !specificFilter || // Проверяем конкретный поиск
        admin.firstName.toLowerCase().includes(specificFilter) ||
        admin.lastName.toLowerCase().includes(specificFilter) ||
        admin.email.toLowerCase().includes(specificFilter) ||
        admin.role.toLowerCase().includes(specificFilter);
      return matchesGlobal && matchesSpecific;
    });
    // Обновляем общее количество страниц при изменении фильтрации
    this.updateTotalPages(filtered.length);
    return filtered;
  }
  get paginatedAdmins(): Admin[] { // Получаем админов для текущей страницы с учетом пагинации
    const filteredData = this.filteredAdmins;
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return filteredData.slice(startIndex, startIndex + this.pageSize);
  }
  updateTotalPages(filteredCount?: number): void { // Обновление общего количества страниц
    const count = filteredCount !== undefined ? filteredCount : this.admins.length;
    this.totalPages = Math.ceil(count / this.pageSize);
    // Если текущая страница больше общего количества страниц, переходим на последнюю
    if (this.currentPage > this.totalPages && this.totalPages > 0) this.currentPage = this.totalPages;
  }
  onPageSizeChange(event: Event): void { // Обработчики для элементов пагинации
    this.pageSize = Number((event.target as HTMLSelectElement).value);
    this.currentPage = 1; // При изменении количества строк сбрасываем на первую страницу
    this.updateTotalPages();
  }

  goToFirstPage(): void { this.currentPage = 1; } // Переход на первую страницу
  goToPreviousPage(): void { // Переход на предыдущую страницу
    if (this.currentPage > 1) this.currentPage--;
  }
  goToNextPage(): void { // Переход на следующую страницу
    if (this.currentPage < this.totalPages) this.currentPage++;
  }
  goToLastPage(): void { this.currentPage = this.totalPages; } // Переход на последнюю страницу
  get pageInfo(): string { // Информация о текущей странице
    const filteredCount = this.filteredAdmins.length;
    if (filteredCount === 0) return '0 - 0 of 0';
    const startIndex = (this.currentPage - 1) * this.pageSize + 1;
    const endIndex = Math.min(startIndex + this.pageSize - 1, filteredCount);
    return `${startIndex} - ${endIndex} of ${filteredCount}`;
  }
  toggleSelectAll(): void { // Выбор всех пользователей на странице
    this.allSelected = !this.allSelected;
    this.paginatedAdmins.forEach(admin => { admin.selected = this.allSelected; });
  }
}