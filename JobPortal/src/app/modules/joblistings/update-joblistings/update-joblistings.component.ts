import { Component, OnInit, inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { JoblistingService } from '../../../services/joblisting.service';
import { ActivatedRoute, Router } from '@angular/router';
import { JobListing } from '../../../models/JobListing.Model';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-update-joblistings',
  templateUrl: './update-joblistings.component.html',
  styleUrl: './update-joblistings.component.css'
})
export class UpdateJoblistingsComponent {
  user : any;
  listing:JobListing;
  router=inject (Router);
  cities: any;
  CurrentDate = new Date();


  constructor(
    private formBuilder: FormBuilder,
    private jobListingService: JoblistingService,
    private toastr: ToastrService,
    private activatedRoute:ActivatedRoute,
    private http: HttpClient

  ) { }
    jobListingForm = this.formBuilder.group({
    jobTitle:new FormControl("null",[Validators.required]) ,
    listingId: new FormControl(null),
    employerId: new FormControl(null, [ Validators.required]),
    jobDescription: new FormControl(null, [ Validators.required]),
    companyName: new FormControl(null, [ Validators.required]),
    hiringWorkflow: new FormControl(null, [ Validators.required]),
    eligibilityCriteria: new FormControl(null, [ Validators.required]),
    requiredSkills: new FormControl(null, [ Validators.required]),
    aboutCompany: new FormControl(null, [ Validators.required]),
    location: new FormControl(null, [ Validators.required]),
    salary: new FormControl(null, [ Validators.required]),
    postedDate: new FormControl(new Date(), [ Validators.required]),
    deadline: new FormControl(null, [Validators.required,this.validateDeadline]),
    vacancyOfJob: new FormControl(null, [ Validators.required])
    // Add more form controls as needed
  });
  ngOnInit(): void {
    
    this.user = JSON.parse(localStorage.getItem('user'))
    this.jobListingForm.patchValue({
      employerId:this.user?.employerId
    })
    this.jobListingService.getListingsById(this.activatedRoute.snapshot.params["id"]).subscribe({
      next:data=>{
        this.listing=data["data"] as JobListing;
        console.log(this.listing)
        this.jobListingForm.patchValue(this.listing as any)
      }
    })
    this.fetchCities();
  }
  updatelistings(){
    this.jobListingService.updateListing(this.jobListingForm.getRawValue(),this.user.token).subscribe({
      next: res => {
        this.toastr.success("Details updated ...")
        console.log(res);
        this.router.navigate(['/joblistings']);
      },
      error : err => {
        this.toastr.error("Update failed..")
        console.log(err);
       }
    })
  }

  validateDeadline(control: AbstractControl): { [key: string]: any } | null {
    const deadline = new Date(control.value);
    const postedDate = new Date();
    if (deadline <= postedDate) {
      return { 'invalidDeadline': true };
    }
    return null;
  }

fetchCities() {
  const username = 'vasanth182801'; // Replace with your Geonames username
  const url = `http://api.geonames.org/searchJSON?country=IN&featureClass=P&maxRows=1000&username=${username}`;
  this.http.get<any>(url).subscribe(
    data => {
      // Extract city names from the response
      const cities = data.geonames.map(city => city.name);
      // Update the form control for the location dropdown
      this.jobListingForm.get('location').setValue(cities[0]); // Set default value
      // Optionally, you can store the list of cities for further use
      this.cities = cities;
    },
    error => {
      console.error('Error fetching cities:', error);
    }
    );
  }
}
