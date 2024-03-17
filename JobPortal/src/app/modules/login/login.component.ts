import { Component,inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthApiService } from '../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  authService : AuthApiService = inject(AuthApiService);
  toastr : ToastrService = inject(ToastrService);
  router : Router = inject(Router);
  showPassword: boolean = false;

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl (null, [Validators.required]),
      accountType : new FormControl("JobSeeker", [Validators.required])
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      // Implement your login logic here
      const email = this.loginForm.value.email;
      const password = this.loginForm.value.password;
      
      if(this.loginForm.get('accountType').value === 'Employer') {
        this.authService.loginEmployer(email, password).subscribe({
          next : (res) => {
            console.log(res);
            this.toastr.success("Login success")
            this.router.navigate(['/home']);
          },
          error: (err) => {
            console.log(err);
            this.toastr.error("Login failed")
          }
        });

      }else{
        this.authService.loginJobSeeker(email, password).subscribe({
          next : (res) => {
            console.log(res);
            this.toastr.success("Login success");
            this.router.navigate(['/home']);
          },
          error: (err) => {
            console.log(err);
            this.toastr.error("Login failed")
          }
        });
  
      }      

    } else {
      this.toastr.error("Please provide a valid registration details");
      console.log("Form validation failed!");  
    }
  }

  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }
}
