import { JobSeeker } from './JobSeeker.Model';

export class Resume {
    resumeId: number;
    jobSeekerId: number;
    resumeUrl: string;
    uploadedDate: Date;
    status: string;
    jobSeeker?:JobSeeker;
    constructor( 
        resumeId: number,
        jobSeekerId: number,
        resumeUrl: string,
        uploadedDate: Date,
        status: string,
        jobSeeker?:JobSeeker )
        {
            this.resumeId = resumeId;
            this.jobSeekerId = jobSeekerId;
            this.resumeUrl = resumeUrl;
            this.uploadedDate = uploadedDate;
            this.status = status;
            this.jobSeeker = jobSeeker;
        }
    }
