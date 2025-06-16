import { Routes } from '@angular/router';
import { HomeComponent } from './Home/home.component';
import { AdminLoginComponent } from './AdminLogin/admin-login.component';
import { AdminLayoutComponent } from './AdminLogin/admin-layout.component';
import { AdminUsersComponent } from './AdminLogin/admin-users/admin-users.component';
import { AdminAdminsComponent } from './AdminLogin/admin-admins/admin-admins.component';
import { AdminProductsComponent } from './AdminLogin/admin-products/admin-products.component';
import { AdminCategoriesComponent } from './AdminLogin/admin-categories/admin-categories.component';
import { AdminProfileComponent } from './AdminLogin/admin-profile/admin-profile.component';
import { AdminSettingsComponent } from './AdminLogin/admin-settings/admin-settings.component';
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
import { NotificationsComponent } from './Profile/notifications/notifications.component';
import { BasketComponent } from './Basket/basket.component';
import { BasketPaymentComponent } from './Basket/basket-payment/basket-payment.component';
import { BasketAddPaymentComponent } from './Basket/basket-add-payment/basket-add-payment.component';
import { WhatIsComponent } from './WhatIs/whatis.component';
import { SupportComponent } from './Profile/support/support.component';
import { SavedCardsComponent } from './Profile/savedCards/savedCards.component';
import { NewaddressComponent } from './Profile/addresses/newaddress/newaddress.component';
import { CreateaddressComponent } from './Profile/addresses/createaddress/createaddress.component';
import { OrdersNoactiveComponent } from './Profile/orders/noactive/orders-noactive.component';
import { DeliveredTabNoactiveComponent } from './Profile/orders/tabs/delivered-tab/noactive/delivered-tab-noactive.component';
import { OrdersTabNoactiveComponent } from './Profile/orders/tabs/orders-tab/noactive/orders-tab-noactive.component';
import { ProcessingTabNoactiveComponent } from './Profile/orders/tabs/processing-tab/noactive/processing-tab-noactive.component';
import { ReturnTabNoactiveComponent } from './Profile/orders/tabs/return-tab/noactive/return-tab-noactive.component';
import { SentTabNoactiveComponent } from './Profile/orders/tabs/sent-tab/noactive/sent-tab-noactive.component';
import { FavoritesNoactiveComponent } from './Profile/favorites/noactive/favorites-noactive.component';
import { HistoryNoactiveComponent } from './Profile/history/noactive/history-noactive.component';
import { CouponsNoactiveComponent } from './Profile/coupons/noactive/coupons-noactive.component';
import { ChatNoactiveComponent } from './Profile/chat/noactive/chat-noactive.component';
import { ReviewsNoactiveComponent } from './Profile/reviews/noactive/reviews-noactive.component';
import { AddressesNoactiveComponent } from './Profile/addresses/noactive/addresses-noactive.component';
import { SavedCardsNoactiveComponent } from './Profile/savedCards/noactive/savedcards-noactive.component';
import { PersonalNoactiveComponent } from './Profile/personal/noactive/personal-noactive.component';
import { SupportNoactiveComponent } from './Profile/support/noactive/support-noactive.component';
import { SellerComponent } from './Profile/seller/seller.component';
import { ProductsAllComponent } from './Profile/seller/products_all/products_all.component';
import { ProductsAddComponent } from './Profile/seller/products_add/products_add.component';
import { SellerOrdersComponent } from './Profile/seller/orders/orders.component';
import { SellerProfileComponent } from './Profile/seller-profile/profile.component'; // Добавляем новый компонент
import { MyProfileComponent } from './Profile/seller-profile/my-profile/my-profile.component';
import { AccountSettingsComponent } from './Profile/seller-profile/account-settings/account-settings.component';
import { StatisticComponent } from './Profile/seller-profile/statistic/statistic.component';
import { DashboardComponent } from './Profile/seller-profile/dashboard/dashboard.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'whatis', component: WhatIsComponent },
  { 
    path: 'basket', 
    children: [
      { path: '', component: BasketComponent },
      { path: 'payment', component: BasketPaymentComponent },
      { path: 'add', component: BasketAddPaymentComponent }
    ]
  },
  {
    path: 'admin',
    children: [
      { path: '', component: AdminLoginComponent },
      {
        path: '',
        component: AdminLayoutComponent,
        children: [
          { path: 'users', component: AdminUsersComponent },
          { path: 'admins', component: AdminAdminsComponent },
          { path: 'products', component: AdminProductsComponent },
          { path: 'categories', component: AdminCategoriesComponent },
          { path: 'profile', component: AdminProfileComponent },
          { path: 'settings', component: AdminSettingsComponent }
        ]
      }
    ]
  },
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
              { path: 'noactive', component: OrdersTabNoactiveComponent }
            ]
          },
          { 
            path: 'processing', 
            component: ProcessingTabComponent,
            children: [
              { path: 'track', component: TrackComponent },
              { path: 'buy-again', component: BuyAgainComponent },
              { path: 'noactive', component: ProcessingTabNoactiveComponent }
            ]
          },
          { 
            path: 'sent', 
            component: SentTabComponent,
            children: [
              { path: 'track', component: TrackComponent },
              { path: 'buy-again', component: BuyAgainComponent },
              { path: 'noactive', component: SentTabNoactiveComponent }
            ]
          },
          { 
            path: 'delivered', 
            component: DeliveredTabComponent,
            children: [
              { path: 'track', component: TrackComponent },
              { path: 'review', component: ReviewComponent },
              { path: 'return-refund', component: ReturnRefundComponent },
              { path: 'buy-again', component: BuyAgainComponent },
              { path: 'noactive', component: DeliveredTabNoactiveComponent }
            ]
          },
          { 
            path: 'return', 
            component: ReturnTabComponent,
            children: [
              { path: 'track', component: TrackComponent },
              { path: 'review', component: ReviewComponent },
              { path: 'buy-again', component: BuyAgainComponent },
              { path: 'noactive', component: ReturnTabNoactiveComponent }
            ]
          },
          { path: 'noactive', component: OrdersNoactiveComponent }
        ]
      },
      { 
        path: 'favorites', 
        component: FavoritesComponent,
        children: [
          { path: 'noactive', component: FavoritesNoactiveComponent }
        ]
      },
      { 
        path: 'history', 
        component: HistoryComponent,
        children: [
          { path: 'noactive', component: HistoryNoactiveComponent }
        ]
      },
      { 
        path: 'coupons', 
        component: CouponsComponent,
        children: [
          { path: 'noactive', component: CouponsNoactiveComponent }
        ]
      },
      { 
        path: 'chat', 
        component: ChatComponent,
        children: [
          { path: 'noactive', component: ChatNoactiveComponent }
        ]
      },
      { 
        path: 'reviews', 
        component: ReviewsComponent,
        children: [
          { path: 'noactive', component: ReviewsNoactiveComponent }
        ]
      },
      { 
        path: 'addresses', 
        component: AddressesComponent,
        children: [
          { path: 'noactive', component: AddressesNoactiveComponent },
          { path: 'newaddress', component: NewaddressComponent },
          { path: 'createaddress/:id', component: CreateaddressComponent }
        ]
      },
      { 
        path: 'saved-cards', 
        component: SavedCardsComponent,
        children: [
          { path: 'noactive', component: SavedCardsNoactiveComponent }
        ]
      },
      { 
        path: 'personal', 
        component: PersonalComponent,
        children: [
          { path: 'noactive', component: PersonalNoactiveComponent }
        ]
      },
      { 
        path: 'support', 
        component: SupportComponent,
        children: [
          { path: 'noactive', component: SupportNoactiveComponent }
        ]
      },
      { path: 'login', component: LoginComponent },
      { path: 'logout', component: LogoutComponent },
    ]
  },
  { 
    path: 'profile/seller', 
    component: SellerComponent,
    children: [
      { path: '', redirectTo: 'products_all', pathMatch: 'full' },
      { path: 'products_all', component: ProductsAllComponent },
      { path: 'products_add', component: ProductsAddComponent },
      { path: 'orders', component: SellerOrdersComponent }
    ]
  },
  { 
    path: 'profile/seller-profile', 
    component: SellerProfileComponent, // Новый компонент для меню
    children: [
      { path: '', redirectTo: 'my-profile', pathMatch: 'full' },
      { path: 'my-profile', component: MyProfileComponent },
      { path: 'account-settings', component: AccountSettingsComponent },
      { path: 'statistic', component: StatisticComponent },
      { path: 'dashboard', component: DashboardComponent }
    ]
  }
];