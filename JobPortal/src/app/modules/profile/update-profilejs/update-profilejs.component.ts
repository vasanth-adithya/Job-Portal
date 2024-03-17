import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { JobseekerService } from '../../../services/jobseeker.service';
import { UpdateJobSeekerDTO } from '../../../models/DTO/UpdateJobSeekerDTO';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-update-profilejs',
  templateUrl: './update-profilejs.component.html',
  styleUrl: './update-profilejs.component.css'
})
export class UpdateProfilejsComponent {
  registerForm: FormGroup;
  jobSeekerService : JobseekerService = inject(JobseekerService);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;
  router:Router=inject (Router);
  toastr : ToastrService = inject(ToastrService);
  user:any;

  ngOnInit() {
    window.scrollTo(0, 0);
    this.registerForm = new FormGroup({
      jobSeekerId: new FormControl(this.user?.jobSeekerId),
      jobSeekerName: new FormControl(null, [Validators.required]),
      userName: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null,  [Validators.required]),
      gender: new FormControl(null,  [Validators.required]),
      contactPhone: new FormControl(null,  [Validators.required]),
      address: new FormControl(null,  [Validators.required]),
      description: new FormControl(null,  [Validators.required]),
      dateOfBirth: new FormControl(null,  [Validators.required]),
      qualification: new FormControl(null,  [Validators.required]),
      specialization: new FormControl(null,  [Validators.required]),
      institute: new FormControl(null,  [Validators.required]),
      year: new FormControl(null,  [Validators.required]),
      cgpa: new FormControl(null,  [Validators.required]),
      companyName: new FormControl(null,  [Validators.required]),
      position: new FormControl(null,  [Validators.required]),
      responsibilities: new FormControl(null,  [Validators.required]),
      startDate: new FormControl(null, [Validators.required]),
      endDate: new FormControl(null, [Validators.required]),
      role: new FormControl(this.user?.role),
      token: new FormControl(this.user?.token)
    },{ validators: endDateAfterStartDateValidator });
    this.user = JSON.parse(localStorage.getItem('user'))
    this.registerForm.patchValue({
      jobSeekerId: this.user.jobSeekerId,
      jobSeekerName: this.user.jobSeekerName,
      userName: this.user.userName,
      email: this.user.email,
      gender: this.user.gender,
      contactPhone: this.user.contactPhone,
      address: this.user.address,
      description:this.user.description,
      dateOfBirth:this.user.dateOfBirth,
      qualification:this.user.qualification,
      specialization:this.user.specialization,
      institute:this.user.institute,
      year:this.user.year,
      cgpa:this.user.cgpa,
      companyName:this.user.companyName,
      position:this.user.position,
      responsibilities:this.user.responsibilities,
      startDate:this.user.startDate,
      endDate:this.user.endDate,
      role: this.user.role,
      token:this.user.token
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const formData: UpdateJobSeekerDTO = this.registerForm.value;
      this.jobSeekerService.updateJobSeeker(formData,this.user.token).subscribe({
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

  return null;
}
