import { Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet, RouterModule  } from '@angular/router';
import { HeaderComponent } from './Header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class AppComponent { title = 'Aliexpress-Front'; }