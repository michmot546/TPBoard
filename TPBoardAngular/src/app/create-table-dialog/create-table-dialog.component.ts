import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TableService } from '../services/table.service';
import { AuthService } from '../services/auth.service';

export interface DialogData {
  projectId: number;
}

@Component({
  selector: 'app-create-table-dialog',
  templateUrl: './create-table-dialog.component.html',
  styleUrls: ['./create-table-dialog.component.css']
})
export class CreateTableDialogComponent {
  createTableForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<CreateTableDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private fb: FormBuilder,
    private tableService: TableService,
    private authService: AuthService
  ) {
    this.createTableForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    if(!this.authService.isAuthenticated())
      this.authService.logout();
  }
  onCancel(): void {
    this.dialogRef.close();
  }

  onCreate(): void {
    if (this.createTableForm.valid) {
      const newTable = {
        id: 0,
        name: this.createTableForm.value.name,
        projectId: this.data.projectId
      };

      this.tableService.createTable(newTable).subscribe({
        next: () => this.dialogRef.close(true),
        error: err => console.error('Failed to create table', err)
      });
    }
  }
}
