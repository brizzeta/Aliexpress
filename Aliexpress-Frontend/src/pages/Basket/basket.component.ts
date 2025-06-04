import { Component, OnInit } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';

interface Product {
  id: number;
  name: string;
  price: number;
  soldCount: number;
  lowestPrice: number;
  imagePath: string;
  rating: number;
}

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [NgFor, NgIf],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent implements OnInit {
  browsingProducts: Product[] = []; // Массивы товаров, который будет отображаться на странице
  products: Product[] = [];
  isLoggedIn: boolean = false; // Новое свойство для отслеживания состояния авторизации

  ngOnInit(): void { 
    this.products = [ // Тестовые данные для товаров
      {
        id: 1,
        name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
        price: 68,
        soldCount: 422,
        lowestPrice: 80,
        imagePath: '../../assets/images/svg/TestBasket.svg',
        rating: this.getRandomRating()
      }, {
        id: 2,
        name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
        price: 72,
        soldCount: 356,
        lowestPrice: 85,
        imagePath: '../../assets/images/svg/TestBasket.svg',
        rating: this.getRandomRating()
      }, {
        id: 3,
        name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
        price: 65,
        soldCount: 510,
        lowestPrice: 76,
        imagePath: '../../assets/images/svg/TestBasket.svg',
        rating: this.getRandomRating()
      }, {
        id: 4,
        name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
        price: 70,
        soldCount: 387,
        lowestPrice: 82,
        imagePath: '../../assets/images/svg/TestBasket.svg',
        rating: this.getRandomRating()
      }, {
        id: 5,
        name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
        price: 70,
        soldCount: 387,
        lowestPrice: 82,
        imagePath: '../../assets/images/svg/TestBasket.svg',
        rating: this.getRandomRating()
      }, {
        id: 6,
        name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
        price: 70,
        soldCount: 387,
        lowestPrice: 82,
        imagePath: '../../assets/images/svg/TestBasket.svg',
        rating: this.getRandomRating()
      }
    ]; 
    this.browsingProducts = [
      {
        id: 1,
        name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
        price: 68,
        soldCount: 422,
        lowestPrice: 80,
        imagePath: '../../assets/images/svg/TestBasket.svg',
        rating: this.getRandomRating()
      }, {
        id: 2,
        name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
        price: 72,
        soldCount: 356,
        lowestPrice: 85,
        imagePath: '../../assets/images/svg/TestBasket.svg',
        rating: this.getRandomRating()
      }, {
        id: 3,
        name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
        price: 65,
        soldCount: 510,
        lowestPrice: 76,
        imagePath: '../../assets/images/svg/TestBasket.svg',
        rating: this.getRandomRating()
      }
    ];
  }
  getRandomRating(): number { return Math.floor(Math.random() * 6); } // от 0 до 5
  login(): void { this.isLoggedIn = true; } // Метод обработки нажатия на "Log In"
  toMain(): void { this.isLoggedIn = false; } // Метод обработки нажатия на "To the main"
}