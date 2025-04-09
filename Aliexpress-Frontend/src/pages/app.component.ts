import { Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { HeaderComponent } from './Header/header.component';
import { FooterComponent } from './Footer/footer.component';  // Импортируем FooterComponent

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, HeaderComponent, FooterComponent],  // Добавляем FooterComponent
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent { 
  title = 'Aliexpress-Front'; 
}
