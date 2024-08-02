import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errorMessage: string = '';
  constructor(private authService: AuthService, private fb: FormBuilder, private router: Router) {
    this.registerForm = this.fb.group({
      login: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      name: [''],
      role: ['User']
    });
  }

  ngOnInit(): void {
    this.registerForm.get('login')?.valueChanges.subscribe(loginValue => {
      this.registerForm.get('name')?.setValue(loginValue);
    });
  }
  
  onRegister() {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe({
        next: () => {
          this.router.navigate(['/board']);
        },
        error: (err) => {
          if (err.status === 409) {
            this.errorMessage = err.error;
          } else {
            this.errorMessage = 'Registration failed';
          }
          console.error('Registration failed', err);
        }
      });
    }
  }
}
