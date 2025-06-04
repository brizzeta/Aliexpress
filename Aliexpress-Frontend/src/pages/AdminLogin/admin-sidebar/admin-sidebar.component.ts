import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, Router, NavigationEnd, Event } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-admin-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './admin-sidebar.component.html',
  styleUrl: './admin-sidebar.component.scss'
})
export class AdminSidebarComponent implements OnInit {
  adminMenuItems = [ // Элементы меню администратора по умолчанию
    { name: 'Users', path: '/admin/users' },
    { name: 'Admins', path: '/admin/admins' },
    { name: 'Products', path: '/admin/products' },
    { name: 'Categories', path: '/admin/categories' }
  ];
  profileMenuItems = [ // Profile/settings элементы меню
    { name: 'My Profile', path: '/admin/profile' },
    { name: 'Account Settings', path: '/admin/settings' },
    { name: 'Back to admin panel', path: '/admin/users' }
  ];
  menuItems = this.adminMenuItems;

  constructor(private router: Router) {}
  ngOnInit() {
    this.updateMenuItems(this.router.url); // Установить начальные пункты меню на основе текущего URL
    this.router.events // Подписка на изменения маршрута, чтобы обновить пункты меню
      .pipe(filter((event: Event): event is NavigationEnd => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => { this.updateMenuItems(event.url); });
  }
  private updateMenuItems(url: string) { // Проверка находимся ли мы на странице профиля или настроек
    if (url.includes('/profile') || url.includes('/settings')) this.menuItems = this.profileMenuItems;
    else this.menuItems = this.adminMenuItems;
  }
}