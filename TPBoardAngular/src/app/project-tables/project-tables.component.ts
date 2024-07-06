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

  constructor(
    private tableElementService: TableElementService,
    private route: ActivatedRoute,
    private tableService: TableService
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
          this.tableElementService.getTableElementsByTableId(table.id).subscribe({
            next: elements => {
              table.elements = elements;
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

  drop(event: CdkDragDrop<TableElement[]>): void {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }
}
