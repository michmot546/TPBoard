import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProjectService } from '../services/project.service';
import { AuthService } from '../services/auth.service';
import { Project } from '../interfaces/project.model';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
})
export class AddProjectComponent {
  projectForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService,
    private authService: AuthService,
    private router: Router
  ) {
    this.projectForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.projectForm.valid) {
      const userId = this.authService.getCurrentUserId();
      if (userId) {
        const newProject: Project = {
          id: 0,
          name: this.projectForm.value.name,
          ownerId: userId,
          users: []
        };

        this.projectService.createProjectWithOwnerAdded(newProject).subscribe({
          next: () => this.router.navigate(['/board']),
          error: err => console.error('Failed to create project', err)
        });
      } else {
        console.error('User is not authenticated');
      }
    }
  }
}
