import { Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { HeaderComponent } from './Header/header.component';
import { FooterComponent } from './Footer/footer.component';  // Импортируем FooterComponent
import { HomeComponent } from './Home/home.component';  // Импортируем HomeComponent

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, HeaderComponent, FooterComponent, HomeComponent],  // Добавляем FooterComponent и HomeComponent
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent { 
  title = 'Aliexpress-Front'; 
}
