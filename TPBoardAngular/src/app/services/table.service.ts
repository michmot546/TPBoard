import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Table } from '../interfaces/table.model';

@Injectable({
  providedIn: 'root'
})
export class TableService {
  private apiUrl = 'https://localhost:7134/api/Table';

  constructor(private http: HttpClient) { }

  getAllTables(): Observable<Table[]> {
    return this.http.get<Table[]>(`${this.apiUrl}/GetAllTables`);
  }

  getTableById(id: number): Observable<Table> {
    return this.http.get<Table>(`${this.apiUrl}/GetTableById/${id}`);
  }
  getTablesByProjectId(projectId: number): Observable<Table[]> {
    return this.http.get<Table[]>(`${this.apiUrl}/GetTablesByProjectId/${projectId}`);
  }
  createTable(table: Table): Observable<Table> {
    return this.http.post<Table>(`${this.apiUrl}/CreateTable`, table); 
  }

  updateTable(table: Table): Observable<Table> {
    return this.http.put<Table>(`${this.apiUrl}/UpdateTable/${table.id}`, table);
  }

  deleteTable(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteTable/${id}`);
  }
}
