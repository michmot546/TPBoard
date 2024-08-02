import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../interfaces/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7134/api/User';

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/GetAllUsers`);
  }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/GetUserById/${id}`);
  }

  createUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/CreateUser`, user);
  }

  updateUser(user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/UpdateUser/${user.id}`, user);
  }

  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteUser/${id}`);
  }
  getUserByName(name: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/GetUserByName/${name}`);
  }
  updateUserName(userId: number, name: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateUserNameSelf`, { id: userId, name });
  }

  updateUserEmail(userId: number, email: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateUserEmailSelf`, { id: userId, email });
  }

  updateUserPassword(userId: number, currentPassword: string, newPassword: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateUserPassword`, { id: userId, currentPassword, newPassword });
  }
  
  // Admin specific updates
  updateUserNameAdmin(userId: number, name: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateUserName`, { id: userId, name });
  }

  updateUserEmailAdmin(userId: number, email: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateUserEmail`, { id: userId, email });
  }
}
