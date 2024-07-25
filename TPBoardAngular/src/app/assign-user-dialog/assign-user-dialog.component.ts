import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectService } from '../services/project.service';
import { User } from '../interfaces/user.model';

@Component({
  selector: 'app-assign-user-dialog',
  templateUrl: './assign-user-dialog.component.html',
})
export class AssignUserDialogComponent implements OnInit {
  users: User[] = [];
  selectedUserId: number | null;

  constructor(
    public dialogRef: MatDialogRef<AssignUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private projectService: ProjectService
  ) {
    this.selectedUserId = null;
  }

  ngOnInit(): void {
    this.loadProjectMembers();
  }

  loadProjectMembers(): void {
    this.projectService.getProjectMembers(this.data.projectId).subscribe({
      next: (users) => {
        this.users = users;
      },
      error: (err) => {
        console.error('Failed to fetch project members:', err);
      }
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  assign(): void {
    this.dialogRef.close(this.selectedUserId);
  }
}
