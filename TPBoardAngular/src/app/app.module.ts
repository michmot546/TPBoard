import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import {MatSidenavModule} from '@angular/material/sidenav';
import { NavbarComponent } from './navbar/navbar.component';
import {MatListModule} from '@angular/material/list';
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
  CdkDrag,
  CdkDropList,
} from '@angular/cdk/drag-drop';
import { SidebarComponent } from './sidebar/sidebar.component';
import {MatIconModule} from '@angular/material/icon';
import {MatGridListModule} from '@angular/material/grid-list';
import { BoardComponent } from './board/board.component';
import { MembersComponent } from './members/members.component';
import { SettingsComponent } from './settings/settings.component';
import { RouterModule } from '@angular/router';
import { RouterTestingModule } from "@angular/router/testing";
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SidebarComponent,
    BoardComponent,
    MembersComponent,
    SettingsComponent,

  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatMenuModule,
    MatSidenavModule,
    MatListModule,
    CdkDropList,
    CdkDrag,
    MatIconModule,
    MatGridListModule,
    RouterModule,
    RouterTestingModule,    
    HttpClientModule
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
