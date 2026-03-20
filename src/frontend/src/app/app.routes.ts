import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'login', loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent) },
  { path: 'register', loadComponent: () => import('./features/auth/register/register.component').then(m => m.RegisterComponent) },
  { path: 'dashboard', loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent), canActivate: [authGuard] },
  { path: 'tables', loadComponent: () => import('./features/tables/table-map/table-map.component').then(m => m.TableMapComponent), canActivate: [authGuard] },
  { path: 'tables/:id', loadComponent: () => import('./features/tables/table-detail/table-detail.component').then(m => m.TableDetailComponent), canActivate: [authGuard] },
  { path: 'orders', loadComponent: () => import('./features/orders/order-list/order-list.component').then(m => m.OrderListComponent), canActivate: [authGuard] },
  { path: 'orders/new/:tableId', loadComponent: () => import('./features/orders/new-order/new-order.component').then(m => m.NewOrderComponent), canActivate: [authGuard] },
  { path: 'orders/:id', loadComponent: () => import('./features/orders/order-detail/order-detail.component').then(m => m.OrderDetailComponent), canActivate: [authGuard] },
  { path: 'menu', loadComponent: () => import('./features/menu/menu-management/menu-management.component').then(m => m.MenuManagementComponent), canActivate: [authGuard] },
  { path: 'payments', loadComponent: () => import('./features/payments/payment.component').then(m => m.PaymentComponent), canActivate: [authGuard] },
  { path: 'settings', loadComponent: () => import('./features/settings/restaurant-settings/restaurant-settings.component').then(m => m.RestaurantSettingsComponent), canActivate: [authGuard] },
  { path: '**', redirectTo: '/dashboard' }
];
