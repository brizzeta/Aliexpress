import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Home/home.component';
import { ProfileComponent } from './Profile/profile.component';
import { LoginComponent } from './Profile/login/login.component';
import { OrdersComponent } from './Profile/orders/orders.component';
import { FavoritesComponent } from './Profile/favorites/favorites.component';
import { HistoryComponent } from './Profile/history/history.component';
import { CouponsComponent } from './Profile/coupons/coupons.component';
import { ChatComponent } from './Profile/chat/chat.component';
import { ReviewsComponent } from './Profile/reviews/reviews.component';
import { AddressesComponent } from './Profile/addresses/addresses.component';
import { PersonalComponent } from './Profile/personal/personal.component';
import { NotificationsComponent } from './Profile/notifications/notifications.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { 
    path: 'profile', 
    children: [
      { path: '', component: ProfileComponent },
      { path: 'login', component: LoginComponent },
      { path: 'orders', component: OrdersComponent },
      { path: 'favorites', component: FavoritesComponent },
      { path: 'history', component: HistoryComponent },
      { path: 'coupons', component: CouponsComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'reviews', component: ReviewsComponent },
      { path: 'addresses', component: AddressesComponent },
      { path: 'personal', component: PersonalComponent },
      { path: 'notifications', component: NotificationsComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }