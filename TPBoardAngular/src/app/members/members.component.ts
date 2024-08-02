import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../services/project.service';
import { RoleService } from '../services/role.service';
import { Project } from '../interfaces/project.model';
import { User } from '../interfaces/user.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  projects: Project[] = [];
  filteredProjects: Project[] = [];
  allUsers: User[] = [];
  filteredUsers: User[] = [];
  newUserName: { [projectId: number]: string } = {};
  userSearchTerm: string = '';
  projectSearchTerm: string = '';
  expandedProjectId: number | null = null;
  maxDisplayedMembers: number = 5;
  errorMessage: string = '';
  userRole: string | null;

  constructor(
    private projectService: ProjectService,
    private userService: UserService,
    private roleService: RoleService
  ) {
    this.userRole = this.roleService.getCurrentUserRole();
  }

  ngOnInit(): void {
    this.loadProjects();
    this.loadAllUsers();
  }

  loadProjects(): void {
    if (this.roleService.isUserAdmin()) {
      this.loadAllProjects();
    } else {
      this.loadUserProjects();
    }
  }

  loadAllProjects(): void {
    this.projectService.getAllProjects().subscribe({
      next: projects => {
        this.projects = projects.map(project => ({
          ...project,
          users: project.users || []
        }));
        this.filteredProjects = this.projects;
        this.loadProjectMembers();
      },
      error: err => console.error('Failed to load all projects', err)
    });
  }

  loadUserProjects(): void {
    this.projectService.getUserProjects().subscribe({
      next: projects => {
        this.projects = projects.map(project => ({
          ...project,
          users: project.users || []
        }));
        this.filteredProjects = this.projects;
        this.loadProjectMembers();
      },
      error: err => console.error('Failed to load user projects', err)
    });
  }

  loadProjectMembers(): void {
    this.projects.forEach(project => {
      this.projectService.getProjectMembers(project.id).subscribe(users => {
        project.users = users.map(user => ({
          userId: user.id,
          user: user,
          projectId: project.id,
          project: project
        }));
      });
    });
  }

  loadAllUsers(): void {
    this.userService.getAllUsers().subscribe({
      next: users => {
        this.allUsers = users;
        this.filteredUsers = users;
      },
      error: err => console.error('Failed to load users', err)
    });
  }

  removeUserFromProject(userId: number, projectId: number): void {
    if (this.userRole === 'Admin' || this.userRole === 'Moderator') {
      this.projectService.removeUserFromProject(userId, projectId).subscribe({
        next: () => {
          const project = this.projects.find(p => p.id === projectId);
          if (project && project.users) {
            project.users = project.users.filter(u => u.userId !== userId);
          }
        },
        error: err => {
          console.error('Failed to remove user from project', err);
          this.errorMessage = err.error?.message || 'An error occurred while removing the user from the project.';
        }
      });
    }
  }

  addUserToProject(userName: string, projectId: number): void {
    if (this.userRole === 'Admin' || this.userRole === 'Moderator') {
      const project = this.projects.find(p => p.id === projectId);
      if (project && project.users) {
        const existingUser = project.users.find(u => u.user.name === userName);
        if (existingUser) {
          this.errorMessage = `User ${userName} is already a member of this project.`;
          return;
        }
      }

      this.userService.getUserByName(userName).subscribe({
        next: user => {
          this.projectService.addUserToProject(user.id, projectId).subscribe({
            next: () => {
              if (project && project.users) {
                project.users.push({
                  userId: user.id,
                  user: user,
                  projectId: project.id,
                  project: project
                });
                this.newUserName[projectId] = '';
                this.errorMessage = '';
              }
            },
            error: err => {
              console.error('Failed to add user to project', err);
              this.errorMessage = err.error?.message || 'An error occurred while adding the user to the project.';
            }
          });
        },
        error: err => {
          console.error('User not found', err);
          this.errorMessage = 'User not found';
        }
      });
    }
  }

  searchProjects(): void {
    this.filteredProjects = this.projects.filter(project =>
      project.name.toLowerCase().includes(this.projectSearchTerm.toLowerCase())
    );
  }

  searchUsers(): void {
    this.filteredUsers = this.allUsers.filter(user =>
      user.name.toLowerCase().includes(this.userSearchTerm.toLowerCase()) ||
      user.email.toLowerCase().includes(this.userSearchTerm.toLowerCase())
    );
  }

  toggleViewAllMembers(projectId: number, event: Event): void {
    event.preventDefault();
    this.expandedProjectId = this.expandedProjectId === projectId ? null : projectId;
  }

  canEditOrDelete(): boolean {
    return this.userRole === 'Admin' || this.userRole === 'Moderator';
  }
}
