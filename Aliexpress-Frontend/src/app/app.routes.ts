import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { LoginComponent } from './profile/login/login.component';
import { OrdersComponent } from './profile/orders/orders.component';
import { FavoritesComponent } from './profile/favorites/favorites.component';
import { HistoryComponent } from './profile/history/history.component';
import { CouponsComponent } from './profile/coupons/coupons.component';
import { ChatComponent } from './profile/chat/chat.component';
import { ReviewsComponent } from './profile/reviews/reviews.component';
import { AddressesComponent } from './profile/addresses/addresses.component';
import { PersonalComponent } from './profile/personal/personal.component';
import { NotificationsComponent } from './profile/notifications/notifications.component';

export const routes: Routes = [{ 
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
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }