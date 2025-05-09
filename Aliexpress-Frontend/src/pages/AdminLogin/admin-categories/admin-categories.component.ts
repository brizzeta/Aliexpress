import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

interface Category {
  name: string;
  subcategory: string;
  selected?: boolean;
}

@Component({
  selector: 'app-admin-categories',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './admin-categories.component.html',
  styleUrl: './admin-categories.component.scss'
})
export class AdminCategoriesComponent implements OnInit {
  categories: Category[] = []; // Массив категорий
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
  isAddFormVisible: boolean = false; // смена флага формы добавления
  isAddFormClosing: boolean = false; // флаг для анимации закрытия
  isEditFormVisible: boolean = false; // смена флага формы добавления
  isEditFormClosing: boolean = false; // флаг для анимации закрытия

  ngOnInit(): void { // Инициализация массива категорий
    this.categories = [
      { name: 'Mobile Phones', subcategory: 'Mobile Phones' },
      { name: 'Mobile Phones', subcategory: 'Mobile Phones Accessories' },
      { name: 'Mobile Phones', subcategory: 'Mobile Phones Parts' },
      { name: 'Mobile Phones', subcategory: 'Sim Cards & Accessories' },
      { name: 'Mobile Phones', subcategory: 'Satellite Phones' },
      { name: 'Computers', subcategory: 'Laptops' },
    ];
    document.body.style.overflow = 'hidden';
    document.addEventListener('click', this.handleDocumentClick.bind(this));
    this.updateTotalPages();
  }

  openAddForm(): void { // Метод открытия формы
    this.isAddFormVisible = true; 
    this.isAddFormClosing = false;
  }
  closeAddForm(event: MouseEvent): void { // Метод закрытия формы
    const target = event.target as HTMLElement; // Проверяем, что клик был по оверлею
    if (target.classList.contains('new-form-overlay')) { 
      this.isAddFormClosing = true; // Сначала активируем анимацию закрытия
      setTimeout(() => {
        this.isAddFormVisible = false; // Фактически скрываем форму после анимации
        this.isAddFormClosing = false;
      }, 300); // Время анимации fadeOut в миллисекундах
    }
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

  get filteredCategories(): Category[] { // Фильтрация категорий
    const globalFilter = this.globalSearchText.toLowerCase();
    const specificFilter = this.searchText.toLowerCase();
    const filtered = this.categories.filter(category => {
      const matchesGlobal = !globalFilter || // Проверяем глобальный поиск
        category.name.toLowerCase().includes(globalFilter) ||
        category.subcategory.toLowerCase().includes(globalFilter);
      const matchesSpecific = !specificFilter || // Проверяем конкретный поиск
        category.name.toLowerCase().includes(globalFilter) ||
        category.subcategory.toLowerCase().includes(globalFilter);
      return matchesGlobal && matchesSpecific;
    });
    // Обновляем общее количество страниц при изменении фильтрации
    this.updateTotalPages(filtered.length);
    return filtered;
  }
  get paginatedCategories(): Category[] { // Получаем категорий для текущей страницы с учетом пагинации
    const filteredData = this.filteredCategories;
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return filteredData.slice(startIndex, startIndex + this.pageSize);
  }
  updateTotalPages(filteredCount?: number): void { // Обновление общего количества страниц
    const count = filteredCount !== undefined ? filteredCount : this.categories.length;
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
    const filteredCount = this.filteredCategories.length;
    if (filteredCount === 0) return '0 - 0 of 0';
    const startIndex = (this.currentPage - 1) * this.pageSize + 1;
    const endIndex = Math.min(startIndex + this.pageSize - 1, filteredCount);
    return `${startIndex} - ${endIndex} of ${filteredCount}`;
  }
  toggleSelectAll(): void { // Выбор всех категорий на странице
    this.allSelected = !this.allSelected;
    this.paginatedCategories.forEach(category => { category.selected = this.allSelected; });
  }
}