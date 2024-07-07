import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../services/project.service';
import { AuthService } from '../services/auth.service';
import { Project } from '../interfaces/project.model';
import { User } from '../interfaces/user.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {
  isAuthenticated: boolean = false;
  projects: (Project & { editing: boolean })[] = [];
  owners: { [key: number]: User } = {};

  constructor(private projectService: ProjectService, private userService: UserService, public authService: AuthService) { }

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    if (this.isAuthenticated) {
      this.projectService.getUserProjects().subscribe({
        next: projects => {
          this.projects = projects.map(project => ({ ...project, editing: false }));
          this.fetchOwners();
        },
        error: err => {
          console.error('Failed to fetch user projects:', err);
        }
      });
    }
  }

  fetchOwners(): void {
    this.projects.forEach(project => {
      if (project.ownerId) {
        this.userService.getUserById(project.ownerId).subscribe({
          next: owner => {
            this.owners[project.ownerId] = owner;
          },
          error: err => {
            console.error('Failed to fetch owner details:', err);
          }
        });
      }
    });
  }

  getOwnerName(ownerId: number): string {
    return this.owners[ownerId]?.name || 'Unknown';
  }

  toggleEdit(project: Project & { editing: boolean }): void {
    if (project.editing) {
      this.projectService.updateProject(project).subscribe({
        next: () => {
          project.editing = false;
          console.log('Project updated successfully');
        },
        error: err => {
          console.error('Failed to update project', err);
        }
      });
    } else {
      project.editing = true;
    }
  }
}
