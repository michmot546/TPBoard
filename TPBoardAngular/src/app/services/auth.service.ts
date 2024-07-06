import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7134/api/User';

  constructor(private http: HttpClient, private router: Router) {}

  login(credentials: { login: string; password: string }): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
      }),
      catchError(this.handleError)
    );
  }

  register(data: { login: string; email: string; password: string; name: string }): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/register`, data).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
      }),
      catchError(this.handleError)
    );
  }

  logout() {
    localStorage.removeItem('token');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (token) {
      const decoded: any = jwtDecode(token);
      if (decoded.exp * 1000 > Date.now()) {
        return true;
      } else {
        this.logout();
      }
    }
    return false;
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 401) {
      this.logout();
      location.reload();
      this.router.navigate(['/login']);
    }
    return throwError(() => error);
  }
}
