import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

interface Admin {
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
  selector: 'app-admin-admins',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
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
  isProfileVisible: boolean = false;
  isProfileClosing: boolean = false;
  isFilterFormVisible: boolean = false; // смена флага формы фильтрации
  isFilterFormClosing: boolean = false; // флаг для анимации закрытия
  isEditFormVisible: boolean = false;
  isEditFormClosing: boolean = false;
  filterButtons: FilterButton[] = [ // Добавим состояние для кнопок фильтрации
    { name: 'Admin', isSelected: false },
    { name: 'Super Admin', isSelected: false }
  ];
  editFilterButtons: FilterButton[] = [
    { name: 'Admin', isSelected: false },
    { name: 'Super Admin', isSelected: false }
  ];

  ngOnInit(): void { // Инициализация массива админов
    this.admins = [
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'super admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'admin' },
      { firstName: 'Женя', lastName: 'Тимофеев', email: 'lokider6@gmail.com', role: 'admin' },
    ];
    document.body.style.overflow = 'hidden';
    document.addEventListener('click', this.handleDocumentClick.bind(this));
    this.updateTotalPages();
  }

  toggleFilterButton(index: number): void { // Метод переключения выбора кнопки фильтра
    this.filterButtons[index].isSelected = !this.filterButtons[index].isSelected;
  }
  openFilterForm(): void { // Метод открытия формы
    this.isFilterFormVisible = true; 
    this.isFilterFormClosing = false;
  }
  closeFilterForm(event: MouseEvent): void { // Метод закрытия формы
    const target = event.target as HTMLElement; // Проверяем, что клик был по оверлею
    if (target.classList.contains('form-overlay')) { 
      this.isFilterFormClosing = true; // Сначала активируем анимацию закрытия
      setTimeout(() => {
        this.isFilterFormVisible = false; // Фактически скрываем форму после анимации
        this.isFilterFormClosing = false;
      }, 300); // Время анимации fadeOut в миллисекундах
    }
  }

  toggleEditFilterButton(index: number): void {
    this.editFilterButtons.forEach(button => { button.isSelected = false; });     // Убираем выделение со всех кнопок
    this.editFilterButtons[index].isSelected = true; // Делаем выбранную кнопку активной
  }
  openEditForm(): void {
    this.isEditFormVisible = true; 
    this.isEditFormClosing = false;
  }
  closeEditForm(event: MouseEvent): void {
    const target = event.target as HTMLElement; // Проверяем, что клик был по оверлею
    if (target.classList.contains('edit-form-overlay')) { 
      this.isEditFormClosing = true; // Сначала активируем анимацию закрытия
      setTimeout(() => {
        this.isEditFormVisible = false; // Фактически скрываем форму после анимации
        this.isEditFormClosing = false;
      }, 300); // Время анимации fadeOut в миллисекундах
    }
  }

  toggleProfile(): void {
    if (this.isProfileVisible)  this.closeProfile();
    else  this.openProfile();
  }
  openProfile(): void {
    this.isProfileVisible = true;
    this.isProfileClosing = false;
  }
  closeProfile(): void {
    this.isProfileClosing = true;
    setTimeout(() => {
      this.isProfileVisible = false;
      this.isProfileClosing = false;
    }, 300);
  }
  handleDocumentClick(event: MouseEvent): void { // Обработчик клика по документу
    const target = event.target as HTMLElement;
    const profileContainer = document.querySelector('.profile');
    const userIcon = document.querySelector('#user');
    // Если клик не по профилю и не по иконке пользователя, закрываем профиль
    if (this.isProfileVisible && profileContainer && userIcon && !profileContainer.contains(target) &&  target !== userIcon) this.closeProfile();
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