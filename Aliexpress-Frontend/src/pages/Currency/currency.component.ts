import { Component } from '@angular/core';

@Component({
  selector: 'app-currency',
  standalone: true,
  imports: [],
  templateUrl: './currency.component.html',
  styleUrls: ['./currency.component.scss']
})
export class CurrencyComponent {
  currencies = [
    { code: 'USD', name: 'USD' },
    { code: 'EUR', name: 'EUR' },
    { code: 'GBP', name: 'GBP' }
  ];

  changeCurrency(currency: any) {
    console.log(`Currency changed to: ${currency.name}`);
    // Логика для изменения валюты
  }
}
