import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, of, BehaviorSubject } from 'rxjs';
import { catchError, tap, switchMap } from 'rxjs/operators';
import { Router } from '@angular/router';

import { UserService } from './user.service'; // Make sure this path is correct
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7134/api/User';
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  constructor(
    private http: HttpClient,
    private router: Router,
    private userService: UserService // Inject UserService
  ) {
    this.isAuthenticated().subscribe();
  }

  login(credentials: { login: string; password: string }): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        const decoded: any = jwtDecode(response.token);
        localStorage.setItem('token', response.token);
        localStorage.setItem('role', decoded.role);
        this.isAuthenticatedSubject.next(true);
      }),
      catchError(this.handleError)
    );
  }

  register(data: { login: string; email: string; password: string; name: string; role: 'User' }): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/register`, data).pipe(
      tap(response => {
        const decoded: any = jwtDecode(response.token);
        localStorage.setItem('token', response.token);
        localStorage.setItem('role', decoded.role);
        this.isAuthenticatedSubject.next(true);
      }),
      catchError(this.handleError)
    );
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.isAuthenticatedSubject.next(false);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string {
    return localStorage.getItem('role')!;
  }

  isAuthenticated(): Observable<boolean> {
    const token = this.getToken();
    if (token) {
      const decoded: any = jwtDecode(token);
      if (decoded.exp * 1000 > Date.now()) {
        this.userService.getUserById(decoded.nameid).subscribe(
          user => {
            if (user) {
              this.isAuthenticatedSubject.next(true);
            } else {
              this.logout();
            }
          },
          () => {
            this.logout();
          }
        );
      } else {
        this.logout();
      }
    } else {
      this.isAuthenticatedSubject.next(false);
    }
    return this.isAuthenticatedSubject.asObservable();
  }

  getCurrentUserId(): number | null {
    const token = this.getToken();
    if (token) {
      const decoded: any = jwtDecode(token);
      return decoded.nameid;
    }
    return null;
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
