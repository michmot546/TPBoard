import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BoardComponent } from './board/board.component';
import { MembersComponent } from './members/members.component';
import { SettingsComponent } from './settings/settings.component';

const routes: Routes = [
  { path: 'board', component: BoardComponent },
  { path: 'members', component: MembersComponent },
  { path: 'settings', component: SettingsComponent },
  { path: '', redirectTo: 'board', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
