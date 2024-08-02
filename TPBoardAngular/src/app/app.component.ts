import { Component, OnInit } from '@angular/core';
import { ProjectService } from './services/project.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'TPBoardAngular';
  isAuthenticated: boolean = false;

  constructor(private projectService: ProjectService, public authService: AuthService) { 
    this.projectService.getAllProjects();
  }

  ngOnInit(): void {
    this.authService.isAuthenticated().subscribe(isAuth => {
      this.isAuthenticated = isAuth;
    });
  }
}
