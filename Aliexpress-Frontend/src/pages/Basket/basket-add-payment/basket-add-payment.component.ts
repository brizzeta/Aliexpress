import { Component } from '@angular/core';

@Component({
  selector: 'app-basket-add-payment',
  standalone: true,
  imports: [],
  templateUrl: './basket-add-payment.component.html',
  styleUrl: './basket-add-payment.component.scss'
})
export class BasketAddPaymentComponent {
  cardNumber: string = '';
  expirationDate: string = '';
  cvvCode: string = '';

  onCardNumberInput(event: Event): void { // Логика для номера карты
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/\D/g, ''); // Удаляем все нецифровые символы
    if (value.length > 16) value = value.substring(0, 16); // Ограничиваем до 16 цифр
    const formattedValue = value.replace(/(\d{4})(?=\d)/g, '$1 '); // Добавляем пробелы каждые 4 цифры
    this.cardNumber = formattedValue; // Обновляем значение
    input.value = formattedValue;
  }
  onCardNumberKeydown(event: KeyboardEvent): void {
    const allowedKeys = [
      'Backspace', 'Delete', 'Tab', 'Escape', 'Enter',
      'ArrowLeft', 'ArrowRight', 'ArrowUp', 'ArrowDown'
    ];
    // Разрешаем только цифры и специальные клавиши
    if (!allowedKeys.includes(event.key) && !/^\d$/.test(event.key)) event.preventDefault();
  }
  onCardNumberPaste(event: ClipboardEvent): void {
    event.preventDefault();
    const paste = event.clipboardData?.getData('text') || '';
    const numbersOnly = paste.replace(/\D/g, '').substring(0, 16);
    
    if (numbersOnly) {
      const formatted = numbersOnly.replace(/(\d{4})(?=\d)/g, '$1 ');
      this.cardNumber = formatted;
      const input = event.target as HTMLInputElement;
      input.value = formatted;
    }
  }
  onExpirationDateInput(event: Event): void { // Логика для даты истечения (MM/YY формат)
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/\D/g, ''); // Удаляем все нецифровые символы
    if (value.length > 4) value = value.substring(0, 4); // Ограничиваем до 4 цифр

    if (value.length >= 2) { // Форматируем в MM/YY
      let month = value.substring(0, 2);
      let year = value.substring(2, 4);

      if (parseInt(month) > 12) month = '12'; // Проверяем корректность месяца (01-12)
      if (parseInt(month) === 0) month = '01';

      const formattedValue = month + (year ? '/' + year : '');
      this.expirationDate = formattedValue;
      input.value = formattedValue;
    } else {
      this.expirationDate = value;
      input.value = value;
    }
  }
  onExpirationDateKeydown(event: KeyboardEvent): void {
    const allowedKeys = [
      'Backspace', 'Delete', 'Tab', 'Escape', 'Enter',
      'ArrowLeft', 'ArrowRight', 'ArrowUp', 'ArrowDown'
    ];
    // Разрешаем только цифры и специальные клавиши
    if (!allowedKeys.includes(event.key) && !/^\d$/.test(event.key)) event.preventDefault();
  }
  onExpirationDatePaste(event: ClipboardEvent): void {
    event.preventDefault();
    const paste = event.clipboardData?.getData('text') || '';
    const numbersOnly = paste.replace(/\D/g, '').substring(0, 4);

    if (numbersOnly) {
      if (numbersOnly.length >= 2) {
        let month = numbersOnly.substring(0, 2);
        let year = numbersOnly.substring(2, 4);
        
        if (parseInt(month) > 12) month = '12'; // Проверяем корректность месяца
        if (parseInt(month) === 0) month = '01';
        
        const formatted = month + (year ? '/' + year : '');
        this.expirationDate = formatted;
        const input = event.target as HTMLInputElement;
        input.value = formatted;
      } else {
        this.expirationDate = numbersOnly;
        const input = event.target as HTMLInputElement;
        input.value = numbersOnly;
      }
    }
  }
  onCvvInput(event: Event): void { // Логика для CVV кода
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/\D/g, ''); // Удаляем все нецифровые символы
    if (value.length > 3) value = value.substring(0, 3); // Ограничиваем до 3 цифр
    this.cvvCode = value;
    input.value = value;
  }
  onCvvKeydown(event: KeyboardEvent): void {
    const allowedKeys = [
      'Backspace', 'Delete', 'Tab', 'Escape', 'Enter',
      'ArrowLeft', 'ArrowRight', 'ArrowUp', 'ArrowDown'
    ];
    // Разрешаем только цифры и специальные клавиши
    if (!allowedKeys.includes(event.key) && !/^\d$/.test(event.key)) event.preventDefault();
  }
  onCvvPaste(event: ClipboardEvent): void {
    event.preventDefault();
    const paste = event.clipboardData?.getData('text') || '';
    const numbersOnly = paste.replace(/\D/g, '').substring(0, 3);
    if (numbersOnly) {
      this.cvvCode = numbersOnly;
      const input = event.target as HTMLInputElement;
      input.value = numbersOnly;
    }
  }
}