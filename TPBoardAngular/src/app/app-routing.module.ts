import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BoardComponent } from './board/board.component';
import { MembersComponent } from './members/members.component';
import { SettingsComponent } from './settings/settings.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AppComponent } from './app.component';
import { authGuard } from './guards/auth.guard';
import { ProjectTablesComponent } from './project-tables/project-tables.component';

const routes: Routes = [
  { path: 'board', component: BoardComponent, canActivate: [authGuard] },
  { path: 'members', component: MembersComponent, canActivate: [authGuard] },
  { path: 'settings', component: SettingsComponent, canActivate: [authGuard] },
  { path: 'project/:id', component: ProjectTablesComponent, canActivate: [authGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', redirectTo: 'board', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
