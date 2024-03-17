import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { EmployerService } from '../../../services/employer.service';
import { UpdateEmployerDTO } from '../../../models/DTO/UpdateEmployerDTO';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css'
})
export class UpdateProfileComponent {
  employerForm: FormGroup;
  toastr : ToastrService = inject(ToastrService);
  employerService : EmployerService = inject(EmployerService);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;
  router:Router=inject (Router);
  user : any;
  ngOnInit(): void {
    this.employerForm = new FormGroup({
      employerId: new FormControl(this.user?.employerId),
      employerName: new FormControl(null, [ Validators.required]),
      userName: new FormControl(null, [ Validators.required]),
      email: new FormControl(null, [ Validators.required, Validators.email]),
      password: new FormControl(null, [ Validators.required]),
      gender: new FormControl(null, [ Validators.required]),
      companyName: new FormControl(null, [ Validators.required]),
      contactPhone: new FormControl(null, [ Validators.required]),
      cwebsiteUrl: new FormControl(null, [ Validators.required]),
      role: new FormControl(this.user?.role),
      token: new FormControl(this.user?.token)
    });
    this.user = JSON.parse(localStorage.getItem('user'))
    this.employerForm.patchValue({
      employerId: this.user.employerId,
      employerName: this.user.employerName,
      userName: this.user.userName,
      email: this.user.email,
      gender: this.user.gender,
      companyName: this.user.companyName,
      contactPhone: this.user.contactPhone,
      cwebsiteUrl: this.user.cwebsiteUrl,
      role: this.user.role,
      token:this.user.token
    });
  }

  onSubmit() {
    console.log(this.user)
    if (this.employerForm.valid) {
      const formData: UpdateEmployerDTO = this.employerForm.value;

      this.employerService.updateEmployer(formData,this.user.token).subscribe({
        next: res => {
          this.toastr.success("Details updated ...")
          console.log(res);
          localStorage.setItem('user',JSON.stringify(res.data))
          this.router.navigate(["/profile"])
        },
        error : err => {
          this.toastr.error("Update failed..")
          console.log(err);
         }
      })
      // Implement your submission logic here
      // console.log(formData);
    } else {
      // Handle form validation errors
      this.toastr.error("Please provide a valid  details");
      console.log("Form validation failed!");
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