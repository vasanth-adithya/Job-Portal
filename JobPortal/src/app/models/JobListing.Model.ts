import { Employer } from './Employer.Model';
import { Application } from './Application.Model';

export class JobListing {
    listingId: number;
    employerId: number;
    jobTitle: string;
    jobDescription: string;
    companyName: string;
    hiringWorkflow: string;
    eligibilityCriteria: string;
    requiredSkills: string;
    aboutCompany: string;
    location: string;
    salary: number;
    postedDate: Date;
    deadline: Date;
    vacancyOfJob?: boolean;
    employer?: Employer;
    applications?:Application[];
    constructor(
        listingId: number,
        employerId: number,
        jobTitle: string,
        jobDescription: string,
        companyName: string,
        hiringWorkflow: string,
        eligibilityCriteria: string,
        requiredSkills: string,
        aboutCompany: string,
        location: string,
        salary: number,
        postedDate: Date,
        deadline: Date,
        vacancyOfJob?: boolean,
        employer?: Employer,
        applications?: Application[]
    ) {
        this.listingId = listingId;
        this.employerId = employerId;
        this.jobTitle = jobTitle;
        this.jobDescription = jobDescription;
        this.companyName = companyName;
        this.hiringWorkflow = hiringWorkflow;
        this.eligibilityCriteria = eligibilityCriteria;
        this.requiredSkills = requiredSkills;
        this.aboutCompany = aboutCompany;
        this.location = location;
        this.salary = salary;
        this.postedDate = postedDate;
        this.deadline = deadline;
        this.vacancyOfJob = vacancyOfJob;
        this.employer = employer;
        this.applications = applications;
    }
}