import { Component, OnInit, inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { JoblistingService } from '../../../services/joblisting.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-post-joblistings',
  templateUrl: './post-joblistings.component.html',
  styleUrl: './post-joblistings.component.css'
})
export class PostJoblistingsComponent implements OnInit {
  jobListingForm: FormGroup;
  user : any;
  router:Router=inject (Router);
  CurrentDate = new Date();
  cities: any;
  constructor(
    private formBuilder: FormBuilder,
    private jobListingService: JoblistingService,
    private toastr: ToastrService,
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.jobListingForm = this.formBuilder.group({
      jobTitle: ['', Validators.required],
      employerId: ['',Validators.required],
      jobDescription: ['', Validators.required],
      companyName: ['',Validators.required],
      hiringWorkflow: ['',Validators.required],
      eligibilityCriteria: ['',Validators.required],
      requiredSkills: ['',Validators.required],
      aboutCompany: ['',Validators.required],
      location: ['',Validators.required],
      salary: ['',Validators.required],
      postedDate: [new Date(),Validators.required],
      deadline: ['',[Validators.required,this.validateDeadline]],
      vacancyOfJob: [true,Validators.required]
      // Add more form controls as needed
    });
    this.user = JSON.parse(localStorage.getItem('user'))
    this.jobListingForm.patchValue({
      employerId:this.user?.employerId,
      companyName:this.user?.companyName
    })
    this.fetchCities();
  }

  submitListing() {
    if (this.jobListingForm.valid) {
      const formData = this.jobListingForm.value;
      // You may need to adjust this depending on your model structure
      this.jobListingService.createListing(formData,this.user.token).subscribe(
        () => {
          this.toastr.success('Job listing posted successfully!');
          this.router.navigate(['/joblistings'])
          // Reset form after successful submission
          this.jobListingForm.reset();
        },
        error => {
          this.toastr.error('Failed to post job listing. Please try again.');
          console.error('Error:', error);
        }
      );
    } else {
      this.toastr.error('Please fill in all required fields.');
    }
  }
  validateDeadline(control: AbstractControl): { [key: string]: any } | null {
    const deadline = new Date(control.value);
    const postedDate = new Date();
    if (deadline <= postedDate) {
      return { 'invalidDeadline': true };
    }
    return null;
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

