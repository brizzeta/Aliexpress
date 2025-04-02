import { Component } from '@angular/core';

@Component({
  selector: 'app-language',
  standalone: true,
  imports: [],
  templateUrl: './language.component.html',
  styleUrls: ['./language.component.scss']
})
export class LanguageComponent {
  languages = [
    { code: 'en', name: 'English' },
    { code: 'es', name: 'Spanish' },
    { code: 'fr', name: 'French' }
  ];

  currencies = [
    { code: 'USD', name: 'USD' },
    { code: 'EUR', name: 'EUR' },
    { code: 'GBP', name: 'GBP' }
  ];

  changeLanguage(language: any) {
    console.log(`Language changed to: ${language.name}`);
    // Логика для изменения языка (например, через i18n или другие механизмы)
  }

  changeCurrency(currency: any) {
    console.log(`Currency changed to: ${currency.name}`);
    // Логика для изменения валюты
  }
}
