import { Routes } from '@angular/router';
import { HomeComponent } from './Home/home.component';
import { ProfileComponent } from './Profile/profile.component';
import { LoginComponent } from './Profile/login/login.component';
import { LogoutComponent } from './Profile/logout/logout.component';
import { OrdersComponent } from './Profile/orders/orders.component';
import { OrdersTabComponent } from './Profile/orders/tabs/orders-tab/orders-tab.component';
import { ProcessingTabComponent } from './Profile/orders/tabs/processing-tab/processing-tab.component';
import { SentTabComponent } from './Profile/orders/tabs/sent-tab/sent-tab.component';
import { DeliveredTabComponent } from './Profile/orders/tabs/delivered-tab/delivered-tab.component';
import { ReturnTabComponent } from './Profile/orders/tabs/return-tab/return-tab.component';
import { TrackComponent } from './Profile/orders/actions/track/track.component';
import { ReviewComponent } from './Profile/orders/actions/review/review.component';
import { ReturnRefundComponent } from './Profile/orders/actions/return-refund/return-refund.component';
import { BuyAgainComponent } from './Profile/orders/actions/buy-again/buy-again.component';
import { FavoritesComponent } from './Profile/favorites/favorites.component';
import { HistoryComponent } from './Profile/history/history.component';
import { CouponsComponent } from './Profile/coupons/coupons.component';
import { ChatComponent } from './Profile/chat/chat.component';
import { ReviewsComponent } from './Profile/reviews/reviews.component';
import { AddressesComponent } from './Profile/addresses/addresses.component';
import { PersonalComponent } from './Profile/personal/personal.component';
import { SupportComponent } from './Profile/support/support.component';
import { SavedCardsComponent } from './Profile/savedCards/savedCards.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { 
    path: 'profile', 
    component: ProfileComponent,
    children: [
      { path: '', redirectTo: 'orders', pathMatch: 'full' },
      { 
        path: 'orders', 
        component: OrdersComponent,
        children: [
          { path: '', redirectTo: 'orders-tab', pathMatch: 'full' },
          { 
            path: 'orders-tab', 
            component: OrdersTabComponent,
            children: [
              { path: 'track', component: TrackComponent },
              { path: 'review', component: ReviewComponent },
              { path: 'return-refund', component: ReturnRefundComponent },
              { path: 'buy-again', component: BuyAgainComponent },
            ]
          },
          { path: 'processing', component: ProcessingTabComponent },
          { path: 'sent', component: SentTabComponent },
          { path: 'delivered', component: DeliveredTabComponent },
          { path: 'return', component: ReturnTabComponent },
        ]
      },
      { path: 'favorites', component: FavoritesComponent },
      { path: 'history', component: HistoryComponent },
      { path: 'coupons', component: CouponsComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'reviews', component: ReviewsComponent },
      { path: 'addresses', component: AddressesComponent },
      { path: 'saved-cards', component: SavedCardsComponent },
      { path: 'personal', component: PersonalComponent },
      { path: 'support', component: SupportComponent },
      { path: 'login', component: LoginComponent },
      { path: 'logout', component: LogoutComponent },
    ]
  }
];