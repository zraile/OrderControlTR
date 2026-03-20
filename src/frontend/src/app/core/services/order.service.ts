import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Order, CreateOrderDto, CreateOrderItemDto, OrderItem, OrderStatus } from '../models/order.model';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getAll(): Observable<Order[]> { return this.http.get<Order[]>(`${this.apiUrl}/orders`); }
  getById(id: number): Observable<Order> { return this.http.get<Order>(`${this.apiUrl}/orders/${id}`); }
  getByTable(tableId: number): Observable<Order[]> { return this.http.get<Order[]>(`${this.apiUrl}/orders/by-table/${tableId}`); }
  getActive(): Observable<Order[]> { return this.http.get<Order[]>(`${this.apiUrl}/orders/active`); }
  create(dto: CreateOrderDto): Observable<Order> { return this.http.post<Order>(`${this.apiUrl}/orders`, dto); }
  updateStatus(id: number, status: OrderStatus): Observable<Order> { return this.http.put<Order>(`${this.apiUrl}/orders/${id}/status`, { status }); }

  getOrderItems(orderId: number): Observable<OrderItem[]> { return this.http.get<OrderItem[]>(`${this.apiUrl}/order-items/by-order/${orderId}`); }
  addOrderItem(dto: CreateOrderItemDto): Observable<OrderItem> { return this.http.post<OrderItem>(`${this.apiUrl}/order-items`, dto); }
  updateOrderItem(id: number, dto: Partial<CreateOrderItemDto>): Observable<OrderItem> { return this.http.put<OrderItem>(`${this.apiUrl}/order-items/${id}`, dto); }
  deleteOrderItem(id: number): Observable<void> { return this.http.delete<void>(`${this.apiUrl}/order-items/${id}`); }
}
