import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Product {
  name: string;
  sku: string;
  stok: string;
  price: string;
  categories: string;
  tags: string;
  date: string;
  selected?: boolean;
}

@Component({
  selector: 'app-admin-products',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-products.component.html',
  styleUrl: './admin-products.component.scss'
})
export class AdminProductsComponent implements OnInit {
  products: Product[] = [];   // Массив продуктов
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

  ngOnInit(): void { // Инициализация массива продуктов
    this.products = [
      { name: 'Product Name', sku: '-', stok: 'in stock', price: '90$', categories: '-', tags: '-', date: '07.04.25'},
      { name: 'Product Name', sku: '-', stok: 'out of stock', price: '65$', categories: '-', tags: '-', date: '04.02.25'},
      { name: 'Product Name', sku: '-', stok: 'in stock', price: '21$', categories: '-', tags: '-', date: '02.01.25'},
      { name: 'Product Name', sku: '-', stok: 'in stock', price: '23$', categories: '-', tags: '-', date: '08.06.2025'},
      { name: 'Product Name', sku: '-', stok: 'in stock', price: '7$', categories: '-', tags: '-', date: '12.04.2025'},
      { name: 'Product Name', sku: '-', stok: 'in stock', price: '125$', categories: '-', tags: '-', date: '23.04.2025'},
    ];
    document.body.style.overflow = 'hidden';
    document.addEventListener('click', this.handleDocumentClick.bind(this));
    this.updateTotalPages();
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

  get filteredProducts(): Product[] { // Фильтрация продуктов
    const globalFilter = this.globalSearchText.toLowerCase();
    const specificFilter = this.searchText.toLowerCase();
    const filtered = this.products.filter(product => {
      const matchesGlobal = !globalFilter || // Проверяем глобальный поиск
        product.name.toLowerCase().includes(globalFilter) ||
        product.sku.toLowerCase().includes(globalFilter) ||
        product.stok.toLowerCase().includes(globalFilter) ||
        product.price.toLowerCase().includes(globalFilter) ||
        product.categories.toLowerCase().includes(globalFilter) ||
        product.tags.toLowerCase().includes(globalFilter) ||
        product.date.toLowerCase().includes(globalFilter);
      const matchesSpecific = !specificFilter || // Проверяем конкретный поиск
        product.name.toLowerCase().includes(globalFilter) ||
        product.sku.toLowerCase().includes(globalFilter) ||
        product.stok.toLowerCase().includes(globalFilter) ||
        product.price.toLowerCase().includes(globalFilter) ||
        product.categories.toLowerCase().includes(globalFilter) ||
        product.tags.toLowerCase().includes(globalFilter) ||
        product.date.toLowerCase().includes(globalFilter);
      return matchesGlobal && matchesSpecific;
    });
    // Обновляем общее количество страниц при изменении фильтрации
    this.updateTotalPages(filtered.length);
    return filtered;
  }
  get paginatedProducts(): Product[] { // Получаем продукты для текущей страницы с учетом пагинации
    const filteredData = this.filteredProducts;
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return filteredData.slice(startIndex, startIndex + this.pageSize);
  }
  updateTotalPages(filteredCount?: number): void {   // Обновление общего количества страниц
    const count = filteredCount !== undefined ? filteredCount : this.products.length;
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
    const filteredCount = this.filteredProducts.length;
    if (filteredCount === 0) return '0 - 0 of 0';
    const startIndex = (this.currentPage - 1) * this.pageSize + 1;
    const endIndex = Math.min(startIndex + this.pageSize - 1, filteredCount);
    return `${startIndex} - ${endIndex} of ${filteredCount}`;
  }
  toggleSelectAll(): void { // Выбор всех продуктов на странице
    this.allSelected = !this.allSelected;
    this.paginatedProducts.forEach(product => { product.selected = this.allSelected; });
  }
}