import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { TableService } from '../services/table.service';
import { TableElementService } from '../services/table-element.service';
import { CreateTableDialogComponent } from '../create-table-dialog/create-table-dialog.component';
import { Table } from '../interfaces/table.model';
import { TableElement } from '../interfaces/tableelement.model';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { CreateElementDialogComponent } from '../create-element-dialog/create-element-dialog.component';

@Component({
  selector: 'app-project-tables',
  templateUrl: './project-tables.component.html',
  styleUrls: ['./project-tables.component.css']
})
export class ProjectTablesComponent implements OnInit {
  tables: Table[] = [];
  projectId: number;
  connectedDropLists: string[] = [];

  constructor(
    private route: ActivatedRoute,
    private tableService: TableService,
    private tableElementService: TableElementService,
    public dialog: MatDialog
  ) {
    this.projectId = +this.route.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.loadTables();
  }

  loadTables(): void {
    this.tableService.getTablesByProjectId(this.projectId).subscribe({
      next: tables => {
        this.tables = tables;
        this.tables.forEach(table => {
          table.elements = table.elements || [];
          this.tableElementService.getTableElementsByTableId(table.id).subscribe({
            next: elements => {
              console.log(`Elements for table ID ${table.id}:`, elements);
              table.elements = elements || [];
              this.connectedDropLists.push(`list-${table.id}`);
            },
            error: err => {
              console.error(`Failed to fetch elements for table ID ${table.id}:`, err);
            }
          });
        });
      },
      error: err => {
        console.error('Failed to fetch tables:', err);
      }
    });
  }

  openCreateTableDialog(): void {
    const dialogRef = this.dialog.open(CreateTableDialogComponent, {
      width: '250px',
      data: { projectId: this.projectId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadTables();
      }
    });
  }

  openCreateElementDialog(table: Table): void {
    const dialogRef = this.dialog.open(CreateElementDialogComponent, {
      width: '250px',
      data: { tableId: table.id }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadTables();
      }
    });
  }

  deleteTable(table: Table): void {
    this.tableService.deleteTable(table.id).subscribe({
      next: () => {
        console.log(`Deleted table ID ${table.id}`);
        this.loadTables();
      },
      error: err => {
        console.error(`Failed to delete table ID ${table.id}:`, err);
      }
    });
  }

  deleteElement(element: TableElement): void {
    this.tableElementService.deleteTableElement(element.id).subscribe({
      next: () => {
        console.log(`Deleted element ID ${element.id}`);
        this.loadTables();
      },
      error: err => {
        console.error(`Failed to delete element ID ${element.id}:`, err);
      }
    });
  }

  drop(event: CdkDragDrop<TableElement[]>, table: Table): void {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );

      const movedElement = event.container.data[event.currentIndex];
      movedElement.tableId = table.id; // Update the tableId for the moved element
      this.tableElementService.updateTableElement(movedElement).subscribe({
        next: () => console.log(`Updated element ID ${movedElement.id}`),
        error: err => console.error(`Failed to update element ID ${movedElement.id}:`, err)
      });
    }

    // Update the table in the backend
    this.tableService.updateTable(table).subscribe({
      next: () => console.log(`Updated table ID ${table.id}`),
      error: err => console.error(`Failed to update table ID ${table.id}:`, err)
    });
  }
}
