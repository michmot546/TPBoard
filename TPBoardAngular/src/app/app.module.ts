import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule} from '@angular/material/sidenav';
import { NavbarComponent } from './navbar/navbar.component';
import { MatListModule, MatSelectionList} from '@angular/material/list';
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
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AppRoutingModule } from './app-routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';

import { AuthInterceptor } from './services/auth-interceptor.service';
import { ProjectTablesComponent } from './project-tables/project-tables.component';
import { TableService } from './services/table.service';
import { TableElementService } from './services/table-element.service';
import { AddProjectComponent } from './add-project/add-project.component';
import { CreateTableDialogComponent } from './create-table-dialog/create-table-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { CreateElementDialogComponent } from './create-element-dialog/create-element-dialog.component';
import { AssignUserDialogComponent } from './assign-user-dialog/assign-user-dialog.component';
import { MatOptionModule } from '@angular/material/core';
import {MatSelectModule} from '@angular/material/select'; 
import { MatTabsModule } from '@angular/material/tabs';
import { AuthService } from './services/auth.service';
import { MembersDialogComponent } from './members-dialog/members-dialog.component';
import { MatTooltipModule } from '@angular/material/tooltip';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SidebarComponent,
    BoardComponent,
    MembersComponent,
    SettingsComponent,
    LoginComponent,
    RegisterComponent,
    ProjectTablesComponent,
    AddProjectComponent,
    CreateTableDialogComponent,
    CreateElementDialogComponent,
    AssignUserDialogComponent,
    MembersDialogComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatMenuModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    CdkDropList,
    CdkDrag,
    MatIconModule,
    MatGridListModule,
    RouterModule,
    RouterTestingModule,    
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    FormsModule,
    MatCardModule,
    MatToolbarModule,
    MatDialogModule,
    MatOptionModule,
    MatSelectModule,
    MatTabsModule,
    MatTooltipModule
  ],
  exports: [RouterModule],
  providers: [
    TableService, 
    TableElementService,
    AuthService, 
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
