import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  changeNameForm: FormGroup;
  changeEmailForm: FormGroup;
  changePasswordForm: FormGroup;
  userId: number | null;
  userRole: string | null;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) {
    this.userId = this.authService.getCurrentUserId();
    this.userRole = this.authService.getRole();

    this.changeNameForm = this.fb.group({
      name: ['', Validators.required]
    });

    this.changeEmailForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });

    this.changePasswordForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  ngOnInit(): void {
    if (!this.userId) {
      this.router.navigate(['/login']);
    }
  }

  onChangeName() {
    if (this.changeNameForm.valid && this.userId) {
      const newName = this.changeNameForm.value.name;
      if (this.userRole === 'Admin') {
        this.userService.updateUserNameAdmin(this.userId, newName).subscribe({
          next: () => {
            this.successMessage = 'Name updated successfully.';
            this.errorMessage = '';
          },
          error: err => {
            this.errorMessage = 'Failed to update name.';
            this.successMessage = '';
            console.error('Failed to update name', err);
          }
        });
      } else {
        this.userService.updateUserName(this.userId, newName).subscribe({
          next: () => {
            this.successMessage = 'Name updated successfully.';
            this.errorMessage = '';
          },
          error: err => {
            this.errorMessage = 'Failed to update name.';
            this.successMessage = '';
            console.error('Failed to update name', err);
          }
        });
      }
    }
  }

  onChangeEmail() {
    if (this.changeEmailForm.valid && this.userId) {
      const newEmail = this.changeEmailForm.value.email;
      if (this.userRole === 'Admin') {
        this.userService.updateUserEmailAdmin(this.userId, newEmail).subscribe({
          next: () => {
            this.successMessage = 'Email updated successfully.';
            this.errorMessage = '';
          },
          error: err => {
            this.errorMessage = 'Failed to update email.';
            this.successMessage = '';
            console.error('Failed to update email', err);
          }
        });
      } else {
        this.userService.updateUserEmail(this.userId, newEmail).subscribe({
          next: () => {
            this.successMessage = 'Email updated successfully.';
            this.errorMessage = '';
          },
          error: err => {
            this.errorMessage = 'Failed to update email.';
            this.successMessage = '';
            console.error('Failed to update email', err);
          }
        });
      }
    }
  }

  onChangePassword() {
    if (this.changePasswordForm.valid && this.userId) {
      const currentPassword = this.changePasswordForm.value.currentPassword;
      const newPassword = this.changePasswordForm.value.newPassword;
      this.userService.updateUserPassword(this.userId, currentPassword, newPassword).subscribe({
        next: () => {
          this.successMessage = 'Password updated successfully.';
          this.errorMessage = '';
        },
        error: err => {
          this.errorMessage = 'Failed to update password.';
          this.successMessage = '';
          console.error('Failed to update password', err);
        }
      });
    }
  }
}
