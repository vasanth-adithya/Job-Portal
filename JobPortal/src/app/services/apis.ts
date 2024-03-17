import { API_URL } from '../../environments/environment';

const BASE_URL : string = API_URL;

// AUTH ENDPOINTS
export const endpoints = {
    SIGNUP_EMPLOYER_API: BASE_URL + "/Auth/Employer/Register",
    SIGNUP_JOBSEEKER_API: BASE_URL + "/Auth/JobSeeker/Register",
    LOGIN_EMPLOYER_API: BASE_URL + "/Auth/Employer/Login",
    LOGIN_JOBSEEKER_API: BASE_URL + "/Auth/JobSeeker/Login",
    LOGOUT_API: BASE_URL + "/Auth/Logout",
    FORGETPASSWORD_API: BASE_URL + "/Auth/ForgetPassword",
    RESETPASSWORD_API: BASE_URL + "/Auth/ResetPassword",
}

//EMPLOYER ENDPOINTS
export const employerEndpoints = {
    GET_ALL_EMPLOYERS_API: BASE_URL + "/Employer/GetAllEmployers",
    GET_EMPLOYER_BY_ID_API: BASE_URL + "/Employer/GetEmployerById/",
    GET_EMPLOYER_BY_EMAIL_API: BASE_URL + "/Employer/GetEmployerByEmail",
    CREATE_EMPLOYER_API: BASE_URL + "/Employer/CreateEmployer",
    UPDATE_EMPLOYER_API: BASE_URL + "/Employer/UpdateEmployer",
    DELETE_EMPLOYER_API: BASE_URL + "/Employer/DeleteEmployer"
  }

// JOBSEEKER ENDPOINTS
export const jobseekerEndpoints = {
    GET_ALL_JOBSEEKERS_API: BASE_URL + "/JobSeeker/GetAllJobSeekers",
    GET_JOBSEEKER_BY_ID_API: BASE_URL + "/JobSeeker/GetJobSeekerById",
    GET_JOBSEEKER_BY_EMAIL_API: BASE_URL + "/JobSeeker/GetJobSeekerByEmail",
    CREATE_JOBSEEKER_API: BASE_URL + "/JobSeeker/CreateJobSeeker",
    UPDATE_JOBSEEKER_API: BASE_URL + "/JobSeeker/UpdateJobSeeker",
    DELETE_JOBSEEKER_API: BASE_URL + "/JobSeeker/DeleteJobSeeker"    
}

// JOBLISTING ENDPOINTS
export const joblistingEndpoints = {
    GET_ALL_JOB_LISTINGS_API: BASE_URL + "/JobListing/GetAllJobListings",
    GET_JOB_LISTINGS_BY_ID_API: BASE_URL + "/JobListing/GetJobListingsById",
    GET_JOB_LISTINGS_BY_EMP_ID_API: BASE_URL + "/JobListing/GetJobListingsByEmployerId",
    CREATE_JOB_LISTING_API: BASE_URL + "/JobListing/CreateJobListing",
    UPDATE_JOB_LISTING_API: BASE_URL + "/JobListing/UpdateJobListing",
    DELETE_JOB_LISTING_API: BASE_URL + "/JobListing/DeleteJobListing"    
}

//APPLICATION ENDPONTS
export const applicationEndpoints = {
    GET_ALL_APPLICATIONS_API: BASE_URL + "/Application/GetAllApplications",
    GET_APPLICATION_BY_ID_API: BASE_URL + "/Application/GetApplicationById",
    GET_APPLICATION_BY_JSID_API: BASE_URL + "/Application/GetApplicationByJSId",
    GET_APPLICATION_BY_EMPID_API: BASE_URL + "/Application/GetApplicationByEmployerId",
    CREATE_APPLICATION_API: BASE_URL + "/Application/CreateApplication",
    UPDATE_APPLICATION_API: BASE_URL + "/Application/UpdateApplication",
    DELETE_APPLICATION_API: BASE_URL + "/Application/DeleteApplication"
  }

//RESUME ENDPOINTS
export const resumeEndpoints = {
    GET_ALL_RESUMES_API: BASE_URL + "/Resume/GetAllResumes",
    GET_RESUME_BY_ID_API: BASE_URL + "/Resume/GetResumeById",
    GET_RESUME_BY_JSID_API: BASE_URL + "/Resume/GetResumeByJSId",
    CREATE_RESUME_API: BASE_URL + "/Resume/CreateResume",
    UPDATE_RESUME_API: BASE_URL + "/Resume/UpdateResume",
    DELETE_RESUME_API: BASE_URL + "/Resume/DeleteResume"
  }