
import { Component } from '@angular/core';
import { ProjectService } from './services/project.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

})
export class AppComponent {
  title = 'TPBoardAngular';
  constructor(private projectService: ProjectService) { }

  ngOnInit(): void {
    // Test the service methods here
    this.projectService.getAllProjects().subscribe(projects => {
      console.log('All projects:', projects);
    });
  }
}
