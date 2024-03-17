import { Component,inject  } from '@angular/core';
import { JoblistingService } from '../../services/joblisting.service';
import { JobListing } from '../../models/JobListing.Model';
import { Application } from '../../models/Application.Model';
import { ApplicationService } from '../../services/application.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-joblistings',
  // templateUrl: './joblistings.component.html',
  templateUrl: './listing.html',

  styleUrl: './joblistings.component.css'
})
export class JoblistingsComponent {
  jobListingService : JoblistingService = inject(JoblistingService);
  applcationService : ApplicationService = inject(ApplicationService);
  jobList;
  filterList;
  user:any;
  toastr : ToastrService = inject(ToastrService);
  router=inject (Router);
  loading : boolean=false;
  isloggedIn=false;
  ngOnInit(): void {
    this.user=JSON.parse(localStorage.getItem('user'))
    this.loading=true;
    if(JSON.parse(localStorage.getItem('user'))!=null)
    {
        this.isloggedIn=true;
    }
    console.log(JSON.parse(localStorage.getItem('user')))
    if(JSON.parse(localStorage.getItem('user'))!=null && JSON.parse(localStorage.getItem('user')).role =="Employer" ){
    const id = JSON.parse(localStorage.getItem('user')).employerId
    this.jobListingService.getListingsByEmpId(id,this.user.token).subscribe(
      (res) => {
        this.jobList = res["data"] as JobListing[];
        this.filterList=this.jobList;
        console.log(this.filterList)
        console.log("api response:" + res);
        this.loading=false
      },
      error => {
        console.log(error);
        this.loading=false
      }
    );
  }
  else{
    this.jobListingService.getAllListings().subscribe(
      (res) => {
        this.jobList = res as JobListing[];
        this.filterList=this.jobList;
        console.log(this.filterList)
        console.log("api response: " + res);
        this.loading=false
      },
      error => {
        console.log(error);
        this.loading=false
      }
    );
  }
}

deletelistings(id:number){
  this.jobListingService.deleteListing(id,this.user.token).subscribe({
    next: res => {
      this.toastr.success("Deleted successfully ...")
      console.log(res);
      setTimeout(() => {
        location.reload();
      }, 500);
        this.ngOnInit();
    },
    error : err => {
      this.toastr.error("Delete failed..")
      console.log(err);
     }
  })
 }

 isJobSeeker(){
  if(JSON.parse(localStorage.getItem('user'))?.role==null)
  {
    return true;
  }
  if(JSON.parse(localStorage.getItem('user')).role=="JobSeeker")
  {
    return true;
  }
  return false;
 }

 apply(listingId:number){
    if(!this.isloggedIn){
      this.toastr.error("Please Login First");
      this.router.navigate(['/login']);
    }
    const application = new Application(
    0,
    listingId,
    JSON.parse(localStorage.getItem('user')).jobSeekerId,
    new Date(),
    "Pending"    
  )
  this.applcationService.createApplication(application,this.user.token).subscribe({
    next:data=>{
      console.log(data);
      this.toastr.success("Applied Job Successfully")
    },
    error:err=>{
      console.log(err);
      this.toastr.error("You cant apply to the job twice")
    }
  });
  console.log(application);

 }

 searchQuery;
 filter(){
  this.filterList=this.jobList.filter((l)=>l.jobTitle.toLowerCase().includes(this.searchQuery.toLowerCase())||l.companyName.toLowerCase().includes(this.searchQuery.toLowerCase())||l.location.toLowerCase().includes(this.searchQuery.toLowerCase())||l.jobDescription.toLowerCase().includes(this.searchQuery.toLowerCase()))
  console.log(this.filterList)
  console.log(this.searchQuery)
 }
 
}
