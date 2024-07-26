import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from '../interfaces/project.model';
import { User } from '../interfaces/user.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private apiUrl = 'https://localhost:7134/api/project';

  constructor(private http: HttpClient) { }

  getAllProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.apiUrl}/GetAllProjects`);
  }

  getProjectById(id: number): Observable<Project> {
    return this.http.get<Project>(`${this.apiUrl}/GetProjectById/${id}`);
  }
  getUserProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.apiUrl}/GetUserProjects`);
  }
  createProject(project: Project): Observable<Project> {
    return this.http.post<Project>(`${this.apiUrl}/CreateProject`, project);
  }
  createProjectWithOwnerAdded(project: Project): Observable<Project> {
    return this.http.post<Project>(`${this.apiUrl}/CreateProjectWithOwnerAdded`, project);
  }
  updateProject(project: Project): Observable<Project> {
    return this.http.put<Project>(`${this.apiUrl}/UpdateProject/${project.id}`, project);
  }

  deleteProject(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteProject/${id}`); 
  }
  getProjectMembers(projectId: number): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/GetProjectMembers/${projectId}`);
  }
  addUserToProject(userId: number, projectId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/AddUserToProject`, { userId, projectId });
  }

  removeUserFromProject(userId: number, projectId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/RemoveUserFromProject`, { userId, projectId });
  }
}
