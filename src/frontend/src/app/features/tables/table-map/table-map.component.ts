import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { TableService } from '../../../core/services/table.service';
import { Table, TableStatus } from '../../../core/models/table.model';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';
import { LoadingSpinnerComponent } from '../../../shared/components/loading-spinner/loading-spinner.component';

@Component({
  selector: 'app-table-map',
  standalone: true,
  imports: [CommonModule, RouterLink, NavbarComponent, LoadingSpinnerComponent],
  templateUrl: './table-map.component.html'
})
export class TableMapComponent implements OnInit {
  tableService = inject(TableService);
  router = inject(Router);

  tables: Table[] = [];
  filteredTables: Table[] = [];
  loading = true;
  filter: 'all' | 'available' | 'occupied' | 'reserved' = 'all';
  TableStatus = TableStatus;

  ngOnInit() {
    this.loadTables();
  }

  loadTables() {
    this.tableService.getAll().subscribe({
      next: (tables) => {
        this.tables = tables;
        this.applyFilter();
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  applyFilter(f?: typeof this.filter) {
    if (f) this.filter = f;
    if (this.filter === 'all') this.filteredTables = this.tables;
    else if (this.filter === 'available') this.filteredTables = this.tables.filter(t => t.status === TableStatus.Available);
    else if (this.filter === 'occupied') this.filteredTables = this.tables.filter(t => t.status === TableStatus.Occupied);
    else this.filteredTables = this.tables.filter(t => t.status === TableStatus.Reserved);
  }

  onTableClick(table: Table) {
    if (table.status === TableStatus.Available) {
      this.router.navigate(['/orders/new', table.id]);
    } else {
      this.router.navigate(['/tables', table.id]);
    }
  }

  getTableClass(status: TableStatus): string {
    if (status === TableStatus.Available) return 'border-green-400 bg-green-50 hover:bg-green-100';
    if (status === TableStatus.Occupied) return 'border-red-400 bg-red-50 hover:bg-red-100';
    return 'border-yellow-400 bg-yellow-50 hover:bg-yellow-100';
  }

  getStatusLabel(status: TableStatus): string {
    if (status === TableStatus.Available) return '🟢 Boş';
    if (status === TableStatus.Occupied) return '🔴 Dolu';
    return '🟡 Rezerve';
  }
}
