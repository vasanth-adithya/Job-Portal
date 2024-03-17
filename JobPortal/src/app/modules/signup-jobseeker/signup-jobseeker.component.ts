import { Component, inject } from '@angular/core';
import { RegisterJobSeekerDTO } from '../../models/DTO/RegisterJobSeekerDTO';
import {  AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AuthApiService } from '../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-signup-jobseeker',
  templateUrl: './signup-jobseeker.component.html',
  styleUrl: './signup-jobseeker.component.css'
})
export class SignupJobseekerComponent {
  registerForm: FormGroup;
  authService : AuthApiService = inject(AuthApiService);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;
  toastr : ToastrService = inject(ToastrService);
  router:Router=inject (Router);
  

  ngOnInit() {
    this.registerForm = new FormGroup({
      jobSeekerName: new FormControl(null, [Validators.required]),
      userName: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null,  [Validators.required]),
      confirmPassword: new FormControl(null,  [Validators.required, this.passwordMatchValidator]),
      gender: new FormControl(null,  [Validators.required]),
      contactPhone: new FormControl(null,  [Validators.required]),
      address: new FormControl(null,  [Validators.required]),
      description: new FormControl(null,  [Validators.required]),
      dateOfBirth: new FormControl(null,  [Validators.required,this.validateDob]),
      qualification: new FormControl(null,  [Validators.required]),
      specialization: new FormControl(null,  [Validators.required]),
      institute: new FormControl(null,  [Validators.required]),
      year: new FormControl(null,  [Validators.required]),
      cgpa: new FormControl(null,  [Validators.required]),
      companyName: new FormControl("N/A",  [Validators.required]),
      position: new FormControl("N/A",  [Validators.required]),
      responsibilities: new FormControl("N/A",  [Validators.required]),
      startDate: new FormControl(new Date(), [Validators.required]),
    endDate: new FormControl(new Date(), [Validators.required]),

    },{ validators: endDateAfterStartDateValidator });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const formData: RegisterJobSeekerDTO = this.registerForm.value;
      this.authService.signupJobSeeker(formData).subscribe({
        next: (res) => {
          setTimeout(() => this.toastr.success('Registration success'), 0);
          console.log(res);
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        error : (err) => {
          this.toastr.error("Registration failed")
          console.log(err);
        }
      })
    } else {
      this.toastr.error("Please provide a valid registration details");
      console.log('Form validation failed');
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

  validateDob(control: AbstractControl): { [key: string]: any } | null {
    const dob = new Date(control.value);
    const Currentyear = new Date().getFullYear()-18;
    const maxDate = new Date(Currentyear,1,1);
    if (dob >= maxDate) {
      return { 'invalidDob': true };
    }
    return null;
  }
}

export function endDateAfterStartDateValidator(control: FormGroup): ValidationErrors | null {
  const startDateControl = control.get('startDate');
  const endDateControl = control.get('endDate');

  if (startDateControl && endDateControl && startDateControl.value && endDateControl.value) {
    const startDate = new Date(startDateControl.value);
    const endDate = new Date(endDateControl.value);

    if (endDate < startDate) {
      return { endDateBeforeStartDate: true };
    }
  }

  return null;
}
