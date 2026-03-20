import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar.component.html'
})
export class SidebarComponent {
  auth = inject(AuthService);

  navItems = [
    { path: '/dashboard', icon: '📊', label: 'Dashboard' },
    { path: '/tables', icon: '🪑', label: 'Masalar' },
    { path: '/orders', icon: '📝', label: 'Siparişler' },
    { path: '/menu', icon: '📋', label: 'Menü Yönetimi' },
    { path: '/payments', icon: '💳', label: 'Ödemeler' },
    { path: '/settings', icon: '⚙️', label: 'Ayarlar' },
  ];
}
