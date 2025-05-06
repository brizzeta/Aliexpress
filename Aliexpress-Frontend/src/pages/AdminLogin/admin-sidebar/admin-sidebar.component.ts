import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './admin-sidebar.component.html',
  styleUrl: './admin-sidebar.component.scss'
})
export class AdminSidebarComponent {
  menuItems = [
    { name: 'Users', path: '/admin/users' },
    { name: 'Admins', path: '/admin/admins' },
    { name: 'Products', path: '/admin/products' },
    { name: 'Categories', path: '/admin/categories' }
  ];
}