import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthApiService } from '../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { ResetPasswordDTO } from '../../models/DTO/ResetPasswordDTO';
import { Router } from '@angular/router';


@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  resetForm: FormGroup;
  authService : AuthApiService = inject(AuthApiService);
  activeRoute: ActivatedRoute = inject(ActivatedRoute);
  toastr : ToastrService = inject(ToastrService);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;
  router : Router = inject(Router);

  ngOnInit() {
    this.resetForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null,  [Validators.required]),
      confirmPassword: new FormControl(null,  [Validators.required, this.passwordMatchValidator]),
    });
  }

  onSubmit() {
    if (this.resetForm.valid) {
      const formData: ResetPasswordDTO = this.resetForm.value;
      formData.token = this.activeRoute.snapshot.paramMap.get("token");
      console.log(formData);
      // Do something with formData, like sending it to the server
      this.authService.resetPassword(formData).subscribe({
        next : (res) => {
          console.log(res);
          this.toastr.success("Password Changed successfully")
          this.router.navigate(['/login']);
        },
        error : (err) => {
          console.log(err);
        }
      })
      console.log(formData);
    } else {
      console.log("Form validation failed!");    
      this.toastr.error("Please provide a valid registration details");
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