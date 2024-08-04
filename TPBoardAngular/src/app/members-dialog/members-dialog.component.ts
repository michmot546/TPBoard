import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Project } from '../interfaces/project.model';
import { RoleService } from '../services/role.service';
import { ProjectService } from '../services/project.service';
import { ProjectUser } from '../interfaces/projectuser.model';

@Component({
  selector: 'app-members-dialog',
  templateUrl: './members-dialog.component.html',
  styleUrls: ['./members-dialog.component.css']
})
export class MembersDialogComponent implements OnInit {
  searchTerm: string = '';
  filteredMembers: ProjectUser[] = [];

  constructor(
    public dialogRef: MatDialogRef<MembersDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { project: Project },
    private roleService: RoleService,
    private projectService: ProjectService
  ) {}

  ngOnInit(): void {
    this.filteredMembers = this.data.project.users;
  }

  searchMembers(): void {
    this.filteredMembers = this.data.project.users.filter(projectUser =>
      projectUser.user.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  isUserAdmin(): boolean {
    return this.roleService.getCurrentUserRole() === 'Admin';
  }

  canEditOrDelete(): boolean {
    return this.roleService.getCurrentUserRole() === 'Admin' || this.roleService.getCurrentUserRole() === 'Moderator';
  }

  removeUserFromProject(userId: number, projectId: number): void {
    this.projectService.removeUserFromProject(userId, projectId).subscribe({
      next: () => {
        this.filteredMembers = this.filteredMembers.filter(user => user.userId !== userId);
        this.data.project.users = this.data.project.users.filter(user => user.userId !== userId);
      },
      error: err => {
        console.error('Failed to remove user from project', err);
      }
    });
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
