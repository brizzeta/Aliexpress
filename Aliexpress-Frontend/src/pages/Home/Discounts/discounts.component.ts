import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-discounts',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './discounts.component.html',
  styleUrls: ['./discounts.component.scss']
})
export class DiscountsComponent {
  currentSlide = 0;
  totalSlides = 3;

  products = [
    // Первый слайд (Bear.jpg)
    {
      image: 'assets/images/Bear.jpg',
      name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
      price: '68$',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '☆'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Bear.jpg',
      name: 'High-Quality Solid Color Leather Texture Phone',
      price: '31$',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '★'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Bear.jpg',
      name: 'Silvery Thorn Rings Spiked Wire Rings',
      price: '2 $',
      sales: '5213 sold',
      rating: ['★', '★', '★', '☆', '☆'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Bear.jpg',
      name: 'Three-dimensional Water Ripples',
      price: '4 $',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '★'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Bear.jpg',
      name: 'MOC White Classic Supercar',
      price: '128$',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '★'],
      oldPrice: '5$'
    },
    // Второй слайд (Knife.jpg)
    {
      image: 'assets/images/Knife.jpg',
      name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
      price: '68$',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '☆'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Knife.jpg',
      name: 'High-Quality Solid Color Leather Texture Phone',
      price: '31$',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '★'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Knife.jpg',
      name: 'Silvery Thorn Rings Spiked Wire Rings',
      price: '2 $',
      sales: '5213 sold',
      rating: ['★', '★', '★', '☆', '☆'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Knife.jpg',
      name: 'Three-dimensional Water Ripples',
      price: '4 $',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '★'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Knife.jpg',
      name: 'MOC White Classic Supercar',
      price: '128$',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '★'],
      oldPrice: '5$'
    },
    // Третий слайд (Lamp.jpg)
    {
      image: 'assets/images/Lamp.jpg',
      name: 'NUOYAQI Men\'s Corduroy Crossbody Bag',
      price: '68$',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '☆'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Lamp.jpg',
      name: 'High-Quality Solid Color Leather Texture Phone',
      price: '31$',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '★'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Lamp.jpg',
      name: 'Silvery Thorn Rings Spiked Wire Rings',
      price: '2 $',
      sales: '5213 sold',
      rating: ['★', '★', '★', '☆', '☆'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Lamp.jpg',
      name: 'Three-dimensional Water Ripples',
      price: '4 $',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '★'],
      oldPrice: '5$'
    },
    {
      image: 'assets/images/Lamp.jpg',
      name: 'MOC White Classic Supercar',
      price: '128$',
      sales: '5213 sold',
      rating: ['★', '★', '★', '★', '★'],
      oldPrice: '5$'
    }
  ];

  get visibleProducts() {
    const start = this.currentSlide * 5;
    return this.products.slice(start, start + 5);
  }

  nextSlide() {
    if (this.currentSlide < this.totalSlides - 1) {
      this.currentSlide++;
    }
  }

  prevSlide() {
    if (this.currentSlide > 0) {
      this.currentSlide--;
    }
  }
}