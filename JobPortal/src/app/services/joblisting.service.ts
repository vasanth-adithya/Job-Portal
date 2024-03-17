import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { JobListing } from '../models/JobListing.Model';
import { joblistingEndpoints } from './apis';
@Injectable({
  providedIn: 'root'
})
export class JoblistingService {

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

  getAllListings(): Observable<JobListing[]> {
    return this.http.get<JobListing[]>(joblistingEndpoints.GET_ALL_JOB_LISTINGS_API)
    .pipe(
      catchError(this.handleError)
    );
  }

  getListingsById(id: number): Observable<JobListing> { 
    return this.http.get<JobListing>(`${joblistingEndpoints.GET_JOB_LISTINGS_BY_ID_API}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  getListingsByEmpId(id: number,token:string): Observable<any> { 
    return this.http.get<JobListing>(`${joblistingEndpoints.GET_JOB_LISTINGS_BY_EMP_ID_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createListing(listing: JobListing,token:string): Observable<JobListing> { 
    return this.http.post<JobListing>(joblistingEndpoints.CREATE_JOB_LISTING_API, listing,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  updateListing(listing: JobListing,token:string): Observable<any> { 
    return this.http.put<any>(joblistingEndpoints.UPDATE_JOB_LISTING_API, listing,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteListing(id: number,token:string): Observable<any> {
    return this.http.delete<any>(`${joblistingEndpoints.DELETE_JOB_LISTING_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
