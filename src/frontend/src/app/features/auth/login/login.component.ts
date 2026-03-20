import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  auth = inject(AuthService);
  router = inject(Router);

  model = { userName: '', password: '' };
  error = '';
  loading = false;

  onSubmit() {
    this.loading = true;
    this.error = '';
    this.auth.login(this.model).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: (err) => {
        this.error = err.error?.message || 'Giriş başarısız. Bilgilerinizi kontrol edin.';
        this.loading = false;
      }
    });
  }
}
