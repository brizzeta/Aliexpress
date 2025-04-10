import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  bolls = [
    { price: '20$', img: 'assets/images/Lamp.svg', alt: 'lamp' },
    { price: '20$', img: 'assets/images/Iron.svg', alt: 'iron' },
    { price: '20$', img: 'assets/images/Socks.svg', alt: 'socks' },
    { price: '20$', img: 'assets/images/Pencils.svg', alt: 'pencils' },
    { price: '20$', img: 'assets/images/Cup.svg', alt: 'cup' }
  ];
}