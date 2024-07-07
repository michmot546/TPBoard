import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TableService } from '../services/table.service';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Table } from '../interfaces/table.model';
import { TableElement } from '../interfaces/tableelement.model';
import { TableElementService } from '../services/table-element.service';

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
    private tableElementService: TableElementService
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
              //console.log(`Elements for table ID ${table.id}:`, elements);
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
        //next: () => console.log(`Updated element ID ${movedElement.id}`),
        //error: err => console.error(`Failed to update element ID ${movedElement.id}:`, err)
      });
    }

    this.tableService.updateTable(table).subscribe({
      //next: () => console.log(`Updated table ID ${table.id}`),
      //error: err => console.error(`Failed to update table ID ${table.id}:`, err)
    });
  }
}
