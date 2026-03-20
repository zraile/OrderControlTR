import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  auth = inject(AuthService);
  router = inject(Router);

  model = { firstName: '', lastName: '', email: '', userName: '', password: '', phoneNumber: '' };
  error = '';
  loading = false;

  onSubmit() {
    this.loading = true;
    this.error = '';
    this.auth.register(this.model).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: (err) => {
        this.error = err.error?.message || 'Kayıt başarısız.';
        this.loading = false;
      }
    });
  }
}
