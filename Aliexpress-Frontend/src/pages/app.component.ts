import { Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { HeaderComponent } from './Header/header.component';
import { FooterComponent } from './Footer/footer.component';  // Импортируем FooterComponent
import { HomeComponent } from './Home/home.component';  // Импортируем HomeComponent
import { TopProductsComponent } from './Home/TopProducts/top-products.component';  // Импортируем TopProductsComponent

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, HeaderComponent, FooterComponent, HomeComponent, TopProductsComponent], 
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent { 
  title = 'Aliexpress-Front'; 
}
