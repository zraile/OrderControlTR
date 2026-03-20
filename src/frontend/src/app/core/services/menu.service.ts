import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { MenuCategory, CreateMenuCategoryDto } from '../models/menu-category.model';
import { MenuItem, CreateMenuItemDto } from '../models/menu-item.model';

@Injectable({ providedIn: 'root' })
export class MenuService {
  private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getCategories(): Observable<MenuCategory[]> { return this.http.get<MenuCategory[]>(`${this.baseUrl}/menu-categories`); }
  getCategoryById(id: number): Observable<MenuCategory> { return this.http.get<MenuCategory>(`${this.baseUrl}/menu-categories/${id}`); }
  createCategory(dto: CreateMenuCategoryDto): Observable<MenuCategory> { return this.http.post<MenuCategory>(`${this.baseUrl}/menu-categories`, dto); }
  updateCategory(id: number, dto: Partial<CreateMenuCategoryDto>): Observable<MenuCategory> { return this.http.put<MenuCategory>(`${this.baseUrl}/menu-categories/${id}`, dto); }
  deleteCategory(id: number): Observable<void> { return this.http.delete<void>(`${this.baseUrl}/menu-categories/${id}`); }

  getItems(): Observable<MenuItem[]> { return this.http.get<MenuItem[]>(`${this.baseUrl}/menu-items`); }
  getItemById(id: number): Observable<MenuItem> { return this.http.get<MenuItem>(`${this.baseUrl}/menu-items/${id}`); }
  getItemsByCategory(categoryId: number): Observable<MenuItem[]> { return this.http.get<MenuItem[]>(`${this.baseUrl}/menu-items/by-category/${categoryId}`); }
  createItem(dto: CreateMenuItemDto): Observable<MenuItem> { return this.http.post<MenuItem>(`${this.baseUrl}/menu-items`, dto); }
  updateItem(id: number, dto: Partial<CreateMenuItemDto>): Observable<MenuItem> { return this.http.put<MenuItem>(`${this.baseUrl}/menu-items/${id}`, dto); }
  deleteItem(id: number): Observable<void> { return this.http.delete<void>(`${this.baseUrl}/menu-items/${id}`); }
}
