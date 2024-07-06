// board.component.ts

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
  projects: Project[] = [];
  owners: { [key: number]: User } = {};  // Object to hold owner details

  constructor(private projectService: ProjectService, private userService: UserService ,public authService: AuthService) { }

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    if (this.isAuthenticated) {
      this.projectService.getUserProjects().subscribe({
        next: projects => {
          this.projects = projects;
          console.log('User projects:', projects);
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
}
