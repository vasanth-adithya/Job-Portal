import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Application } from '../models/Application.Model';
import { applicationEndpoints } from './apis'; 
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

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
  getAllApplications(token:string): Observable<Application[]>{
    return this.http.get<Application[]>(applicationEndpoints.GET_ALL_APPLICATIONS_API,this.setToken(token))
    .pipe(
      catchError(this.handleError)
    );
  }

  getApplicationById(id: number,token:string): Observable<Application> { 
    return this.http.get<Application>(`${applicationEndpoints.GET_APPLICATION_BY_ID_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getApplicationByJSId(id: number,token:string): Observable<Application> { 
    return this.http.get<Application>(`${applicationEndpoints.GET_APPLICATION_BY_JSID_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getApplicationByEmployerId(id: number,token:string): Observable<any> { 
    return this.http.get<Application>(`${applicationEndpoints.GET_APPLICATION_BY_EMPID_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createApplication(application: Application,token:string): Observable<any> { 
    return this.http.post<Application>(applicationEndpoints.CREATE_APPLICATION_API, application,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  updateApplication(application: Application,token:string): Observable<any> { 
    return this.http.put<any>(applicationEndpoints.UPDATE_APPLICATION_API, application,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteApplication(id: number,token:string): Observable<any> {
    return this.http.delete<any>(`${applicationEndpoints.DELETE_APPLICATION_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');

  }

}
