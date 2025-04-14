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
  discounts = [
    { price: '15$', img: 'assets/images/Lamp.svg', alt: 'lamp' },
    { price: '15$', img: 'assets/images/Iron.svg', alt: 'iron' },
    { price: '15$', img: 'assets/images/Socks.svg', alt: 'socks' },
    { price: '15$', img: 'assets/images/Pencils.svg', alt: 'pencils' },
    { price: '15$', img: 'assets/images/Cup.svg', alt: 'cup' }
  ];
}