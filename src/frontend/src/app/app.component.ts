import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './core/services/auth.service';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, SidebarComponent],
  template: `
    <div class="flex h-screen bg-gray-100">
      @if (auth.isLoggedIn()) {
        <app-sidebar></app-sidebar>
        <div class="flex-1 ml-64 flex flex-col overflow-hidden">
          <router-outlet></router-outlet>
        </div>
      } @else {
        <router-outlet></router-outlet>
      }
    </div>
  `
})
export class AppComponent {
  auth = inject(AuthService);
}
