import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { JobseekerService } from '../../../services/jobseeker.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { JobSeeker } from '../../../models/JobSeeker.Model';
import { ResumeService } from '../../../services/resume.service';
import { Resume } from '../../../models/Resume.Model';

@Component({
  selector: 'app-list-jobseeker',
  templateUrl: './list-jobseeker.component.html',
  styleUrl: './list-jobseeker.component.css'
})
export class ListJobseekerComponent {
  constructor(
    private formBuilder: FormBuilder,
    private jobSeekerService: JobseekerService,
    private toastr: ToastrService,
    private activatedRoute:ActivatedRoute,
    private resumeService:ResumeService
    ) { }
    router=inject (Router)
  
    user:any
    resumedata:any
    ngOnInit(){
      this.user = JSON.parse(localStorage.getItem('user'))
      this.jobSeekerService.getJobSeekerById(this.activatedRoute.snapshot.params["id"],this.user.token).subscribe({
        next:data=>{
          this.user=data as JobSeeker;
          console.log(data)
          this.toastr.success(" Fetching the details..")
        },
        error : err => {
          this.toastr.error("Failed Fetching the details..")
          console.log(err);
         }
      })

      this.resumeService.getResumeByJSId(this.activatedRoute.snapshot.params["id"],this.user.token).subscribe({
        next:data=>{
          this.resumedata=data["data"][0] as Resume;
          console.log(data)
        },
        error:err=>{
          console.log(err);
        }
      })
  }
}
