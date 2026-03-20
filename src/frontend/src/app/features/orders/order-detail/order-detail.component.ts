import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { OrderService } from '../../../core/services/order.service';
import { PaymentService } from '../../../core/services/payment.service';
import { Order, OrderStatus } from '../../../core/models/order.model';
import { PaymentType } from '../../../core/models/payment.model';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';
import { CurrencyTrPipe } from '../../../shared/pipes/currency-tr.pipe';

@Component({
  selector: 'app-order-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, NavbarComponent, CurrencyTrPipe],
  templateUrl: './order-detail.component.html'
})
export class OrderDetailComponent implements OnInit {
  route = inject(ActivatedRoute);
  router = inject(Router);
  orderService = inject(OrderService);
  paymentService = inject(PaymentService);

  order: Order | null = null;
  loading = true;
  showPayment = false;
  paymentType = PaymentType.Cash;
  PaymentType = PaymentType;
  OrderStatus = OrderStatus;
  paying = false;

  ngOnInit() {
    const id = +this.route.snapshot.params['id'];
    this.orderService.getById(id).subscribe({
      next: (order) => { this.order = order; this.loading = false; },
      error: () => this.loading = false
    });
  }

  getStatusLabel(status: OrderStatus): string {
    const labels: Record<number, string> = { 1: 'Yeni', 2: 'Hazırlanıyor', 3: 'Hazır', 4: 'Teslim Edildi', 5: 'İptal' };
    return labels[status] || '';
  }

  updateStatus(status: OrderStatus) {
    if (!this.order) return;
    this.orderService.updateStatus(this.order.id, status).subscribe(updated => this.order = updated);
  }

  completePayment() {
    if (!this.order) return;
    this.paying = true;
    this.paymentService.create({
      orderId: this.order.id,
      amount: this.order.totalAmount,
      paymentType: this.paymentType
    }).subscribe({
      next: () => {
        this.updateStatus(OrderStatus.Delivered);
        this.router.navigate(['/tables']);
      },
      error: () => this.paying = false
    });
  }
}
