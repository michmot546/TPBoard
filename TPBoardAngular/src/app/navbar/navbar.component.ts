import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  userRole: string | null = null;
  userName: string = "";

  constructor(private authService: AuthService, private router: Router, private userService: UserService) {}

  ngOnInit(): void {
    this.userRole = this.authService.getRole();
    const userId = this.authService.getCurrentUserId();
    if (userId !== null) {
      this.userService.getUserName(userId).subscribe({
        next: (response) => {
          this.userName = response.name;
        },
        error: err => {
          console.error('Failed to retrieve user name', err);
        }
      });
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  canAddProject(): boolean {
    return this.userRole === 'Admin' || this.userRole === 'Moderator';
  }
}
