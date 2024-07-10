import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TableElementService } from '../services/table-element.service';

@Component({
  selector: 'app-create-element-dialog',
  templateUrl: './create-element-dialog.component.html',
  styleUrls: ['./create-element-dialog.component.css']
})
export class CreateElementDialogComponent {
  elementForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<CreateElementDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { tableId: number },
    private fb: FormBuilder,
    private tableElementService: TableElementService
  ) {
    this.elementForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.elementForm.valid) {
      const newElement = {
        id: 0,
        name: this.elementForm.value.name,
        tableId: this.data.tableId
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
