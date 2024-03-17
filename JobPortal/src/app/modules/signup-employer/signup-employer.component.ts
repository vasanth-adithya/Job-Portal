import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterEmployerDTO } from '../../models/DTO/RegisterEmployerDTO';
import { AuthApiService } from '../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-signup-employer',
  templateUrl: './signup-employer.component.html',
  styleUrl: './signup-employer.component.css'
})
export class SignupEmployerComponent {
  employerForm: FormGroup;
  toastr : ToastrService = inject(ToastrService);
  authService : AuthApiService = inject(AuthApiService);
  router:Router=inject (Router);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;

  ngOnInit(): void {
    this.employerForm = new FormGroup({
      employerName: new FormControl(null, [ Validators.required]),
      userName: new FormControl(null, [ Validators.required]),
      email: new FormControl(null, [ Validators.required, Validators.email]),
      password: new FormControl(null, [ Validators.required]),
      confirmPassword: new FormControl(null, [ Validators.required]),
      gender: new FormControl(null, [ Validators.required]),
      companyName: new FormControl(null, [ Validators.required]),
      contactPhone: new FormControl(null, [ Validators.required]),
      cwebsiteUrl: new FormControl(null, [ Validators.required])
    });
  }

  onSubmit() {
    if (this.employerForm.valid) {
      const formData: RegisterEmployerDTO = this.employerForm.value;

      this.authService.signupEmployer(formData).subscribe({
        next: (res) => {
          setTimeout(() => this.toastr.success('Registration success'), 0);
          console.log(res);
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        error: (err) => {
          this.toastr.error('Registration failed');
          console.log(err);
        },
      });
      // Implement your submission logic here
      // console.log(formData);
    } else {
      // Handle form validation errors
      this.toastr.error('Please provide a valid registration details');
      console.log('Form validation failed!');
    }
  }
  toggleShowConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }
}