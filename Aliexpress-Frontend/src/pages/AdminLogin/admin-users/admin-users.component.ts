import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface User {
  firstName: string;
  lastName: string;
  email: string;
  role: string;
  selected?: boolean;
}
interface FilterButton {
  name: string;
  isSelected: boolean;
}

@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.scss'
})
export class AdminUsersComponent implements OnInit {
  users: User[] = []; // Массив пользователей
  // Для фильтрации
  globalSearchText: string = '';
  searchText: string = '';
  // Для пагинации
  currentPage: number = 1;
  pageSize: number = 5;
  pageSizeOptions: number[] = [1, 3, 5];
  totalPages: number = 1;
  allSelected: boolean = false; // Выбор чекбоксов
  isFormVisible: boolean = false; // смена флага формы фильтрации
  isFormClosing: boolean = false; // флаг для анимации закрытия
  filterButtons: FilterButton[] = [ // Добавим состояние для кнопок фильтрации
    { name: 'Customer', isSelected: false },
    { name: 'Seller', isSelected: false }
  ];
  
  ngOnInit(): void { // Инициализация массива пользователей
    this.users = [
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'super admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'seller' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'customer' }
    ];
    document.body.style.overflow = 'hidden';
    this.updateTotalPages();
  }

  toggleFilterButton(index: number): void { // Метод переключения выбора кнопки фильтра
    this.filterButtons[index].isSelected = !this.filterButtons[index].isSelected;
  }
  openForm(): void { // Метод открытия формы
    this.isFormVisible = true; 
    this.isFormClosing = false;
  }
  closeForm(event: MouseEvent): void { // Метод закрытия формы
    const target = event.target as HTMLElement; // Проверяем, что клик был по оверлею
    if (target.classList.contains('form-overlay')) { 
      this.isFormClosing = true; // Сначала активируем анимацию закрытия
      setTimeout(() => {
        this.isFormVisible = false; // Фактически скрываем форму после анимации
        this.isFormClosing = false;
      }, 300); // Время анимации fadeOut в миллисекундах
    }
  }
  
  get filteredUsers(): User[] { // Фильтрация пользователей
    const globalFilter = this.globalSearchText.toLowerCase();
    const specificFilter = this.searchText.toLowerCase();
    const filtered = this.users.filter(user => {
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
    // Обновляем общее количество страниц при изменении фильтрации
    this.updateTotalPages(filtered.length);
    return filtered;
  }
  get paginatedUsers(): User[] { // Получаем пользователей для текущей страницы с учетом пагинации
    const filteredData = this.filteredUsers;
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return filteredData.slice(startIndex, startIndex + this.pageSize);
  }
  updateTotalPages(filteredCount?: number): void { // Обновление общего количества страниц
    const count = filteredCount !== undefined ? filteredCount : this.users.length;
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
    const filteredCount = this.filteredUsers.length;
    if (filteredCount === 0) return '0 - 0 of 0';
    const startIndex = (this.currentPage - 1) * this.pageSize + 1;
    const endIndex = Math.min(startIndex + this.pageSize - 1, filteredCount);
    return `${startIndex} - ${endIndex} of ${filteredCount}`;
  }
  toggleSelectAll(): void { // Выбор всех пользователей на странице
    this.allSelected = !this.allSelected;
    this.paginatedUsers.forEach(user => { user.selected = this.allSelected; });
  }
}