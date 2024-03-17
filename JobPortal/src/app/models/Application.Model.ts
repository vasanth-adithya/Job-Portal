import { JobSeeker } from './JobSeeker.Model';
import { JobListing } from './JobListing.Model';

export class Application{
    applicationId: number;
    listingId: number;
    jobSeekerId: number;
    applicationDate: Date;
    applicationStatus: string;
    jobSeeker?:JobSeeker;
    listing?:JobListing;
    
    constructor(applicationId: number, listingId: number, jobSeekerId: number, applicationDate: Date, applicationStatus: string, jobSeeker?: JobSeeker, listing?: JobListing) {
        this.applicationId = applicationId;
        this.listingId = listingId;
        this.jobSeekerId = jobSeekerId;
        this.applicationDate = applicationDate;
        this.applicationStatus = applicationStatus;
        this.jobSeeker = jobSeeker;
        this.listing = listing;
    }
}
// export interface IApplication{
//     applicationId: number;
//     listingId: number;
//     jobSeekerId: number;
//     applicationDate: Date;
//     applicationStatus: string;
//     jobSeeker:Record<string,any>;
//     listing:Record<string,any>;
    
// }