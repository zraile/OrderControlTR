import { Component, OnInit, inject, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DashboardService } from '../../core/services/dashboard.service';
import { OrderService } from '../../core/services/order.service';
import { NavbarComponent } from '../../shared/components/navbar/navbar.component';
import { LoadingSpinnerComponent } from '../../shared/components/loading-spinner/loading-spinner.component';
import { CurrencyTrPipe } from '../../shared/pipes/currency-tr.pipe';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, NavbarComponent, LoadingSpinnerComponent, CurrencyTrPipe],
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit, AfterViewInit {
  @ViewChild('salesChart') salesChartRef!: ElementRef;
  @ViewChild('paymentChart') paymentChartRef!: ElementRef;

  dashboardService = inject(DashboardService);
  orderService = inject(OrderService);

  summary: any = null;
  recentOrders: any[] = [];
  salesData: any[] = [];
  paymentData: any[] = [];
  tablesData: any = null;
  loading = true;

  ngOnInit() {
    this.loadData();
  }

  ngAfterViewInit() {
    setTimeout(() => this.initCharts(), 500);
  }

  loadData() {
    this.dashboardService.getSummary().subscribe({
      next: (data) => { this.summary = data; },
      error: () => { this.summary = { totalSales: 0, orderCount: 0, activeTableCount: 0, averageOrderAmount: 0 }; }
    });

    this.orderService.getAll().subscribe({
      next: (orders) => { this.recentOrders = orders.slice(0, 10); this.loading = false; },
      error: () => { this.loading = false; }
    });

    const end = new Date().toISOString().split('T')[0];
    const start = new Date(Date.now() - 7 * 24 * 60 * 60 * 1000).toISOString().split('T')[0];
    this.dashboardService.getSalesByDateRange(start, end).subscribe({
      next: (data) => { this.salesData = data; setTimeout(() => this.initCharts(), 100); },
      error: () => {}
    });

    this.dashboardService.getPaymentTypesSummary().subscribe({
      next: (data) => { this.paymentData = data; setTimeout(() => this.initCharts(), 100); },
      error: () => {}
    });

    this.dashboardService.getTablesStatusSummary().subscribe({
      next: (data) => { this.tablesData = data; },
      error: () => {}
    });
  }

  initCharts() {
    if (this.salesChartRef?.nativeElement) {
      const existing = Chart.getChart(this.salesChartRef.nativeElement);
      if (existing) existing.destroy();
      new Chart(this.salesChartRef.nativeElement, {
        type: 'bar',
        data: {
          labels: this.salesData.length ? this.salesData.map((d: any) => d.date || d.label || '') : ['Pzt', 'Sal', 'Çar', 'Per', 'Cum', 'Cmt', 'Paz'],
          datasets: [{
            label: 'Satışlar (₺)',
            data: this.salesData.length ? this.salesData.map((d: any) => d.total || d.amount || 0) : [0, 0, 0, 0, 0, 0, 0],
            backgroundColor: 'rgba(34, 197, 94, 0.7)',
            borderColor: 'rgba(34, 197, 94, 1)',
            borderWidth: 1
          }]
        },
        options: { responsive: true, plugins: { legend: { display: false } } }
      });
    }

    if (this.paymentChartRef?.nativeElement) {
      const existing = Chart.getChart(this.paymentChartRef.nativeElement);
      if (existing) existing.destroy();
      new Chart(this.paymentChartRef.nativeElement, {
        type: 'doughnut',
        data: {
          labels: this.paymentData.length ? this.paymentData.map((d: any) => d.paymentType || d.type || d.label) : ['Nakit', 'Kart', 'Online'],
          datasets: [{
            data: this.paymentData.length ? this.paymentData.map((d: any) => d.total || d.amount || d.count || 0) : [0, 0, 0],
            backgroundColor: ['#22c55e', '#3b82f6', '#a855f7']
          }]
        },
        options: { responsive: true }
      });
    }
  }

  getOrderStatusLabel(status: number): string {
    const labels: Record<number, string> = { 1: 'Yeni', 2: 'Hazırlanıyor', 3: 'Hazır', 4: 'Teslim Edildi', 5: 'İptal' };
    return labels[status] || 'Bilinmiyor';
  }

  getOrderStatusClass(status: number): string {
    const classes: Record<number, string> = {
      1: 'bg-blue-100 text-blue-700',
      2: 'bg-orange-100 text-orange-700',
      3: 'bg-green-100 text-green-700',
      4: 'bg-gray-100 text-gray-700',
      5: 'bg-red-100 text-red-700'
    };
    return classes[status] || 'bg-gray-100 text-gray-700';
  }
}
