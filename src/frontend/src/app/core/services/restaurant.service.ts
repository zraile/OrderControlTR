import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Restaurant, CreateRestaurantDto } from '../models/restaurant.model';

@Injectable({ providedIn: 'root' })
export class RestaurantService {
  private apiUrl = `${environment.apiUrl}/restaurants`;
  constructor(private http: HttpClient) {}
  getAll(): Observable<Restaurant[]> { return this.http.get<Restaurant[]>(this.apiUrl); }
  getById(id: number): Observable<Restaurant> { return this.http.get<Restaurant>(`${this.apiUrl}/${id}`); }
  create(dto: CreateRestaurantDto): Observable<Restaurant> { return this.http.post<Restaurant>(this.apiUrl, dto); }
  update(id: number, dto: Partial<CreateRestaurantDto>): Observable<Restaurant> { return this.http.put<Restaurant>(`${this.apiUrl}/${id}`, dto); }
  delete(id: number): Observable<void> { return this.http.delete<void>(`${this.apiUrl}/${id}`); }
}
