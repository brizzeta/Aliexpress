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
    { price: '20$', img: 'assets/images/Lamp.png', alt: 'lamp' },
    { price: '20$', img: 'assets/images/Iron.png', alt: 'iron' },
    { price: '20$', img: 'assets/images/Socks.png', alt: 'socks' },
    { price: '20$', img: 'assets/images/Pencils.png', alt: 'pencils' },
    { price: '20$', img: 'assets/images/Cup.png', alt: 'cup' }
  ];
}