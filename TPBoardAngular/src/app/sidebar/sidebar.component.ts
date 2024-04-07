import { CdkDrag, CdkDragDrop, CdkDropList, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, ComponentFactoryResolver, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { BoardComponent } from '../board/board.component';
import { MembersComponent } from '../members/members.component';
import { SettingsComponent } from '../settings/settings.component';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {

  @ViewChild('componentContainer', { read: ViewContainerRef, static: true }) componentContainer!: ViewContainerRef;

  constructor(private componentFactoryResolver: ComponentFactoryResolver) { }

  ngOnInit() {
    if (this.componentContainer) {
      this.loadComponent('board');
    }
  }

  loadComponent(componentName: string) {
    this.componentContainer.clear();
    let component: any;
    switch (componentName) {
      case 'board':
        component = BoardComponent;
        break;
      case 'members':
        component = MembersComponent;
        break;
      case 'settings':
        component = SettingsComponent;
        break;
      default:
        component = null;
    }
    if (component) {
      const componentFactory = this.componentFactoryResolver.resolveComponentFactory(component);
      this.componentContainer.createComponent(componentFactory);
    }
  }

}
