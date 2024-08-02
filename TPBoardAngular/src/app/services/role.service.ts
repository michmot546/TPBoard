import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { ProjectService } from './project.service';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { User } from '../interfaces/user.model';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  constructor(
    private authService: AuthService,
    private projectService: ProjectService
  ) {}

  getCurrentUserRole(): string | null {
    return this.authService.getRole();
  }

  getCurrentUserId(): number | null {
    return this.authService.getCurrentUserId();
  }

  isUserInRole(role: string): boolean {
    const currentUserRole = this.getCurrentUserRole();
    return currentUserRole === role;
  }

  isUserAdmin(): boolean {
    return this.isUserInRole('Admin');
  }

  isUserModerator(): boolean {
    return this.isUserInRole('Moderator');
  }

  isUserRegularUser(): boolean {
    return this.isUserInRole('User');
  }

  isUserProjectMember(projectId: number): Observable<boolean> {
    const userId = this.getCurrentUserId();
    if (userId === null) {
      return of(false);
    }

    return this.projectService.getProjectMembers(projectId).pipe(
      map((users: User[]) => users.some(user => user.id === userId)),
      catchError(() => of(false))
    );
  }
}
