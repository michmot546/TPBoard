import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Table } from '../interfaces/table.model';

@Injectable({
  providedIn: 'root'
})
export class TableService {
  private apiUrl = 'http://localhost:7134/api/Table';

  constructor(private http: HttpClient) { }

  getAllTables(): Observable<Table[]> {
    return this.http.get<Table[]>(this.apiUrl);
  }

  getTableById(id: number): Observable<Table> {
    return this.http.get<Table>(`${this.apiUrl}/${id}`);
  }

  createTable(Table: Table): Observable<Table> {
    return this.http.post<Table>(this.apiUrl, Table);
  }

  updateTable(Table: Table): Observable<Table> {
    return this.http.put<Table>(`${this.apiUrl}/${Table.id}`, Table);
  }

  deleteTable(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
