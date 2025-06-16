import { Component, Output, EventEmitter, Input, HostBinding, ViewChildren, QueryList, ElementRef } from '@angular/core';
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
  @ViewChildren('input0, input1, input2, input3') inputs!: QueryList<ElementRef>;
  @Input() 
  isLoginForm = true;
  isRecoveryForm = false;
  isCodeForm = false;
  isPasswordForm = false;

  closeModal(): void { this.close.emit(); } // Метод закрытия окна
  // Метод для предотвращения закрытия при клике на login-container
  preventClose(event: Event): void { event.stopPropagation(); }

  toggleForm(): void {
    this.isLoginForm = !this.isLoginForm;
    this.isRecoveryForm = false;
    this.isCodeForm = false;
    this.isPasswordForm = false;
  }
  showRecoveryForm(): void {
    this.isLoginForm = false;
    this.isRecoveryForm = true;
    this.isCodeForm = false;
    this.isPasswordForm = false;
  }
  showCodeForm(): void {
    this.isRecoveryForm = false;
    this.isCodeForm = true;
    this.isPasswordForm = false;
    this.isLoginForm = false;
  }
  showPasswordForm(): void {
    this.isLoginForm = false;
    this.isRecoveryForm = false;
    this.isCodeForm = false;
    this.isPasswordForm = true;
  }
  showLoginForm(): void {
    this.isLoginForm = true;
    this.isRecoveryForm = false;
    this.isCodeForm = false;
    this.isPasswordForm = false;
  }

  onInputChange(event: any, index: number) { // Метод для обработки ввода в поля кода
    const input = event.target, value = input.value;
    
    if (!/^\d*$/.test(value)) { // Принимаем только цифры
      input.value = '';
      return;
    }
    // Если пользователь ввел цифру, переходим к следующему полю
    if (value.length === 1 && index < 3) {
      setTimeout(() => {
        this.inputs.toArray()[index + 1].nativeElement.focus();
      }, 10);
    }
    // Для случая удаления (backspace), когда поле пустое, возвращаемся к предыдущему
    if (value.length === 0 && index > 0) {
      setTimeout(() => {
        this.inputs.toArray()[index - 1].nativeElement.focus();
      }, 10);
    }
  }
  getCode(): string { // Метод для получения полного кода
    return this.inputs 
      ? this.inputs.map(el => el.nativeElement.value).join('')
      : '';
  }
}