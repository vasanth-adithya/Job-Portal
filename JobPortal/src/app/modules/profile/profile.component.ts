import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { JobSeeker } from '../../models/JobSeeker.Model';
import { CloudinaryService } from '../../services/cloudinary.service';
import { Resume } from '../../models/Resume.Model';
import { ICloudinaryUploadResponse } from '../../models/ICloudinaryInterface';
import { ResumeService } from '../../services/resume.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent{
  user : any;
  resume!: File | null;
  router : Router = inject(Router);
  cloudinaryService=inject(CloudinaryService)
  resumeService=inject(ResumeService)
  @ViewChild('file') fileInput!: ElementRef;
  toastr : ToastrService = inject(ToastrService);
  ngDoCheck(){
    this.user = JSON.parse(localStorage.getItem('user'));
  }
  onFileChange(event: Event) {
    this.resume = (event.target as HTMLInputElement).files![0];
    console.log("File: " + this.resume.name);
}

  uploadResume(){
    
    if(this.resumedata!=null){
      var choice = null;
      if (this.resumedata!=null){
      choice = window.confirm("You already have a resume.Do you wish to replace it with new one?")
    }
  }
    this.resumeUploader(choice)
}
  resumedata: any;
  ngOnInit(){
    this.user = JSON.parse(localStorage.getItem('user'))
    const user=JSON.parse(localStorage.getItem("user"))
    this.resumeService.getResumeByJSId(user.jobSeekerId,this.user.token).subscribe({
      next:data=>{
        this.resumedata=data["data"][0] as Resume;
      },
      error:err=>{
        console.log(err);
      }
    })
  }
  resumeUploader(choice:any){
    const resdata : Resume= {
      resumeId:0,
      jobSeekerId:this.user?.jobSeekerId,
      resumeUrl:"",
      uploadedDate: new Date(),
      status: "Active"
    }
    console.log(this.resume)
    this.cloudinaryService.uploadResume(this.resume).subscribe({
      
      next:data=>{
        console.log(data);
        const d = data as ICloudinaryUploadResponse;
        resdata.resumeUrl=d.url;
        console.log(choice)
        if(choice==null){
        this.resumeService.createResume(resdata,this.user.token).subscribe({
          next:data=>{
            this.toastr.success("Resume Created Successfully");
            setTimeout(() => {
              location.reload();
            }, 500);
            console.log(data)
          },
          error:e=>{
            console.log(e);
          },
        })
      }
      else if(choice==true){
        console.log(resdata.resumeUrl)
        resdata.resumeId=this.resumedata.resumeId;
        resdata.uploadedDate=new Date();
        this.resumeService.updateResume(resdata,this.user.token).subscribe({
          next:data=>{
            console.log(data)
            setTimeout(() => {
              location.reload();
          }, 500);
          },
          error:e=>{
            console.log(e);
            this.toastr.error("Resume Updation failed");

          },
        }
        )
      }
      },
      error:err=>{
        console.log(err);
      },
      // complete:()=>{
      //   this.router.navigate(['/profile'])
      // }
    })
  }

 deleteResume(){
  this.resumeService.deleteResume(this.resumedata.resumeId,this.user.token).subscribe({
    next:data=>{
      this.toastr.success("Resume Deleted Successfully")
      console.log(data)
      setTimeout(() => {
        location.reload();
      }, 500);
      // this.resumeService.getResumeByJSId(this.user.jobSeekerId).subscribe({
      //   next:data=>{
      //     this.resumedata=data["data"][0] as Resume;
      //     console.log(data)
      //   },
      //   error:err=>{
      //     console.log(err);
      //   }
      // })
    },
    error:err => {
      console.log(err);
      this.toastr.error("Delete Failed")
    },
  })
 }
  }

