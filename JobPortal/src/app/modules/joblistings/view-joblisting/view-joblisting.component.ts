import { Component, Input, inject } from '@angular/core';
import { JoblistingService } from '../../../services/joblisting.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { JobListing } from '../../../models/JobListing.Model';

@Component({
  selector: 'app-view-joblisting',
  // templateUrl: './view-joblisting.component.html',
  templateUrl: './view.html',
  styleUrl: './view-joblisting.component.css'
})
export class ViewJoblistingComponent {
  @Input() listing: any; 
  router:Router=inject (Router);
 constructor(
  private formBuilder: FormBuilder,
  private jobListingService: JoblistingService,
  private toastr: ToastrService,
  private activatedRoute:ActivatedRoute

 ){}
//  listing:any
 user:any
 
 ngOnInit(): void {
  this.user = JSON.parse(localStorage.getItem('user'))
  this.jobListingService.getListingsById(this.activatedRoute.snapshot.params["id"]).subscribe({
    next:data=>{
      this.listing=data["data"] as JobListing;
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
}
