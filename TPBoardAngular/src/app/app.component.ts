
import { Component, OnInit } from '@angular/core';
import { ProjectService } from './services/project.service';
import { AuthService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

})
export class AppComponent implements OnInit{
  title = 'TPBoardAngular';
  isAuthenticated: boolean = false;
  
  constructor(private projectService: ProjectService, public authService: AuthService) { }

  ngOnInit(): void {
    // Test the service methods here
    this.isAuthenticated = this.authService.isAuthenticated();
    if(this.isAuthenticated==true){
    this.projectService.getAllProjects().subscribe(projects => {
      console.log('All projects:', projects);
    });
  }
  }
}
