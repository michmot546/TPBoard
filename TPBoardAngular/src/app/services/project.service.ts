import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from '../interfaces/project.model';

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

  updateProject(project: Project): Observable<Project> {
    return this.http.put<Project>(`${this.apiUrl}/UpdateProject/${project.id}`, project);
  }

  deleteProject(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteProject/${id}`); 
  }
}
