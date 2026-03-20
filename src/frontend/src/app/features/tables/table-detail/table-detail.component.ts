import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { TableService } from '../../../core/services/table.service';
import { OrderService } from '../../../core/services/order.service';
import { Table } from '../../../core/models/table.model';
import { Order } from '../../../core/models/order.model';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';
import { CurrencyTrPipe } from '../../../shared/pipes/currency-tr.pipe';

@Component({
  selector: 'app-table-detail',
  standalone: true,
  imports: [CommonModule, NavbarComponent, CurrencyTrPipe],
  templateUrl: './table-detail.component.html'
})
export class TableDetailComponent implements OnInit {
  route = inject(ActivatedRoute);
  router = inject(Router);
  tableService = inject(TableService);
  orderService = inject(OrderService);

  table: Table | null = null;
  orders: Order[] = [];
  loading = true;

  ngOnInit() {
    const id = +this.route.snapshot.params['id'];
    this.tableService.getById(id).subscribe(t => this.table = t);
    this.orderService.getByTable(id).subscribe({
      next: (orders) => { this.orders = orders; this.loading = false; },
      error: () => this.loading = false
    });
  }

  newOrder() {
    this.router.navigate(['/orders/new', this.table?.id]);
  }

  goBack() {
    this.router.navigate(['/tables']);
  }
}
