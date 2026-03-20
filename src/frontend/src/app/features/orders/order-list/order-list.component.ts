import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { OrderService } from '../../../core/services/order.service';
import { Order, OrderStatus } from '../../../core/models/order.model';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';
import { CurrencyTrPipe } from '../../../shared/pipes/currency-tr.pipe';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, NavbarComponent, CurrencyTrPipe],
  templateUrl: './order-list.component.html'
})
export class OrderListComponent implements OnInit {
  orderService = inject(OrderService);
  orders: Order[] = [];
  filtered: Order[] = [];
  loading = true;
  statusFilter = '';
  OrderStatus = OrderStatus;

  ngOnInit() {
    this.orderService.getAll().subscribe({
      next: (orders) => { this.orders = orders; this.filtered = orders; this.loading = false; },
      error: () => this.loading = false
    });
  }

  applyFilter() {
    if (!this.statusFilter) this.filtered = this.orders;
    else this.filtered = this.orders.filter(o => o.status === +this.statusFilter);
  }

  getStatusLabel(status: OrderStatus): string {
    const labels: Record<number, string> = { 1: 'Yeni', 2: 'Hazırlanıyor', 3: 'Hazır', 4: 'Teslim Edildi', 5: 'İptal' };
    return labels[status] || '';
  }

  getStatusClass(status: OrderStatus): string {
    const classes: Record<number, string> = {
      1: 'bg-blue-100 text-blue-700',
      2: 'bg-orange-100 text-orange-700',
      3: 'bg-green-100 text-green-700',
      4: 'bg-gray-100 text-gray-700',
      5: 'bg-red-100 text-red-700'
    };
    return classes[status] || '';
  }

  updateStatus(order: Order, status: OrderStatus) {
    this.orderService.updateStatus(order.id, status).subscribe(updated => {
      order.status = updated.status;
    });
  }
}
