import { Component, inject } from '@angular/core';
import { ApplicationService } from '../../../services/application.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Application } from '../../../models/Application.Model';

@Component({
  selector: 'app-update-application',
  templateUrl: './update-application.component.html',
  styleUrl: './update-application.component.css'
})
export class UpdateApplicationComponent {
  constructor(
    private formBuilder: FormBuilder,
    private applicationService: ApplicationService,
    private toastr: ToastrService,
    private activatedRoute:ActivatedRoute,
    ) { }
    router=inject (Router)
    user:any;
    applicationform = this.formBuilder.group({
    applicationId:new FormControl(null,[Validators.required]),
    listingId: new FormControl(null, [ Validators.required]),
    jobSeekerId: new FormControl(null, [ Validators.required]),
    applicationDate: new FormControl(null, [ Validators.required]),
    applicationStatus: new FormControl(null, [ Validators.required]),
    // Add more form controls as needed
  });
  application: any
  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user'))
    this.applicationService.getApplicationById(this.activatedRoute.snapshot.params["id"],this.user.token).subscribe({
      next:data=>{
        this.application=data["data"] as Application;
        console.log(this.application)
        this.applicationform.patchValue(this.application as any)
      }
    })
    this.applicationform.get("listingId").disable(),
    this.applicationform.get("jobSeekerId").disable(),
    this.applicationform.get("applicationId").disable(),
    this.applicationform.get("applicationDate").disable()
  }


  updateApplications(){
    this.applicationService.updateApplication(this.applicationform.getRawValue(),this.user.token).subscribe({
      next: res => {
        this.toastr.success("Details updated ...")
        console.log(res);
        this.router.navigate(['/dashboard']);
      },
      error : err => {
        this.toastr.error("Update failed..")
        console.log(err);
       }
    })
  }
}
