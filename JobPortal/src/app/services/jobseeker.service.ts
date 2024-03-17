import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { JobSeeker } from '../models/JobSeeker.Model';
import { jobseekerEndpoints } from './apis';
import { UpdateJobSeekerDTO } from '../models/DTO/UpdateJobSeekerDTO';

@Injectable({
  providedIn: 'root'
})
export class JobseekerService {

  
  constructor(private http: HttpClient) { }

  setToken(token: string ){
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
    return httpOptions;
  }

  getAllJobSeekers(token:string): Observable<JobSeeker[]>{
    return this.http.get<JobSeeker[]>(jobseekerEndpoints.GET_ALL_JOBSEEKERS_API,this.setToken(token))
    .pipe(
      catchError(this.handleError)
    );
  }

  getJobSeekerById(id: number,token:string): Observable<JobSeeker> { 
    return this.http.get<JobSeeker>(`${jobseekerEndpoints.GET_JOBSEEKER_BY_ID_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getJobSeekerByEmail(email: string,token:string): Observable<JobSeeker> { 
    return this.http.get<JobSeeker>(`${jobseekerEndpoints.GET_JOBSEEKER_BY_EMAIL_API}/${email}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createJobSeeker(jobseeker: JobSeeker,token:string): Observable<JobSeeker> { 
    return this.http.post<JobSeeker>(jobseekerEndpoints.CREATE_JOBSEEKER_API, jobseeker,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  updateJobSeeker(jobseeker: UpdateJobSeekerDTO,token:string): Observable<any> { 
    return this.http.put<any>(jobseekerEndpoints.UPDATE_JOBSEEKER_API, jobseeker,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteJobSeeker(id: number,token:string): Observable<any> {
    return this.http.delete<any>(`${jobseekerEndpoints.DELETE_JOBSEEKER_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}

