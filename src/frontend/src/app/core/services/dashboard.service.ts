import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class DashboardService {
  private apiUrl = `${environment.apiUrl}/dashboard`;
  constructor(private http: HttpClient) {}
  getSummary(): Observable<any> { return this.http.get<any>(`${this.apiUrl}/summary`); }
  getSalesByDateRange(startDate: string, endDate: string): Observable<any> { return this.http.get<any>(`${this.apiUrl}/sales-by-date-range?startDate=${startDate}&endDate=${endDate}`); }
  getPaymentTypesSummary(): Observable<any> { return this.http.get<any>(`${this.apiUrl}/payment-types-summary`); }
  getTablesStatusSummary(): Observable<any> { return this.http.get<any>(`${this.apiUrl}/tables-status-summary`); }
}
