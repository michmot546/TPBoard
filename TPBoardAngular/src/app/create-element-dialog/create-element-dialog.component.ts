import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TableElementService } from '../services/table-element.service';
import { ProjectService } from '../services/project.service';
import { User } from '../interfaces/user.model';

@Component({
  selector: 'app-create-element-dialog',
  templateUrl: './create-element-dialog.component.html',
  styleUrls: ['./create-element-dialog.component.css']
})
export class CreateElementDialogComponent implements OnInit {
  elementForm: FormGroup;
  users: User[] = [];

  constructor(
    public dialogRef: MatDialogRef<CreateElementDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { tableId: number, projectId: number },
    private fb: FormBuilder,
    private tableElementService: TableElementService,
    private projectService: ProjectService
  ) {
    this.elementForm = this.fb.group({
      name: ['', Validators.required],
      assignedUserId: [null]
    });
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

  onSubmit(): void {
    if (this.elementForm.valid) {
      const newElement = {
        id: 0,
        name: this.elementForm.value.name,
        tableId: this.data.tableId,
        assignedUserId: this.elementForm.value.assignedUserId
      };

      this.tableElementService.createTableElement(newElement).subscribe({
        next: () => this.dialogRef.close(true),
        error: err => console.error('Failed to create element', err)
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
