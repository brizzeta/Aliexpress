import { Component, Output, EventEmitter, Input, HostBinding } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  @Output() close = new EventEmitter<void>();
  @HostBinding('class.active') active = true;
  @Input() 
  closeModal(): void { this.close.emit(); } // Метод закрытия окна
  // Метод для предотвращения закрытия при клике на login-container
  preventClose(event: Event): void { event.stopPropagation(); }
}