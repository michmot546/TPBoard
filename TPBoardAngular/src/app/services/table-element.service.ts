import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TableElement } from '../interfaces/tableelement.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TableElementService {
  private apiUrl = 'http://localhost:7134/api/TableElement';

  constructor(private http: HttpClient) { }

  getAllTableElements(): Observable<TableElement[]> {
    return this.http.get<TableElement[]>(`${this.apiUrl}/GetAllTableElements`);
  }

  getTableElementById(id: number): Observable<TableElement> {
    return this.http.get<TableElement>(`${this.apiUrl}/GetTableElementById/${id}`);
  }

  createTableElement(tableElement: TableElement): Observable<TableElement> {
    return this.http.post<TableElement>(`${this.apiUrl}/CreateTableElement`, tableElement);
  }

  updateTableElement(tableElement: TableElement): Observable<TableElement> {
    return this.http.put<TableElement>(`${this.apiUrl}/UpdateTableElement/${tableElement.id}`, tableElement);
  }

  deleteTableElement(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteTableElement/${id}`);
  }
}
