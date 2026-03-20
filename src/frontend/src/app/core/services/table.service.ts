import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Table, CreateTableDto, TableStatus } from '../models/table.model';

@Injectable({ providedIn: 'root' })
export class TableService {
  private apiUrl = `${environment.apiUrl}/tables`;
  constructor(private http: HttpClient) {}
  getAll(): Observable<Table[]> { return this.http.get<Table[]>(this.apiUrl); }
  getById(id: number): Observable<Table> { return this.http.get<Table>(`${this.apiUrl}/${id}`); }
  create(dto: CreateTableDto): Observable<Table> { return this.http.post<Table>(this.apiUrl, dto); }
  update(id: number, dto: Partial<CreateTableDto>): Observable<Table> { return this.http.put<Table>(`${this.apiUrl}/${id}`, dto); }
  updateStatus(id: number, status: TableStatus): Observable<Table> { return this.http.put<Table>(`${this.apiUrl}/${id}/status`, { status }); }
  delete(id: number): Observable<void> { return this.http.delete<void>(`${this.apiUrl}/${id}`); }
}
