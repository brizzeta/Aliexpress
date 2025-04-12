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
    { price: '20$', img: 'assets/images/png/Lamp.png', alt: 'lamp' },
    { price: '20$', img: 'assets/images/png/Iron.png', alt: 'iron' },
    { price: '20$', img: 'assets/images/png/Socks.png', alt: 'socks' },
    { price: '20$', img: 'assets/images/png/Pencils.png', alt: 'pencils' },
    { price: '20$', img: 'assets/images/png/Cup.png', alt: 'cup' }
  ];
}