import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentService } from '../../core/services/payment.service';
import { Payment, PaymentType } from '../../core/models/payment.model';
import { NavbarComponent } from '../../shared/components/navbar/navbar.component';
import { CurrencyTrPipe } from '../../shared/pipes/currency-tr.pipe';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, NavbarComponent, CurrencyTrPipe],
  templateUrl: './payment.component.html'
})
export class PaymentComponent implements OnInit {
  paymentService = inject(PaymentService);
  payments: Payment[] = [];
  loading = true;
  PaymentType = PaymentType;

  ngOnInit() {
    this.paymentService.getAll().subscribe({
      next: (p) => { this.payments = p; this.loading = false; },
      error: () => this.loading = false
    });
  }

  getPaymentTypeLabel(type: PaymentType): string {
    const labels: Record<number, string> = { 1: '💵 Nakit', 2: '💳 Kredi Kartı', 3: '📱 Online' };
    return labels[type] || 'Bilinmiyor';
  }
}
