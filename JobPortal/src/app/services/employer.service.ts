import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Employer } from '../models/Employer.Model';
import { employerEndpoints } from './apis';
import { UpdateEmployerDTO } from '../models/DTO/UpdateEmployerDTO';

@Injectable({
  providedIn: 'root'
})
export class EmployerService {

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

  getAllEmployers(token:string): Observable<Employer[]>{
    return this.http.get<Employer[]>(employerEndpoints.GET_ALL_EMPLOYERS_API,this.setToken(token))
    .pipe(
      catchError(this.handleError)
    );
  }

  getEmployerById(id: number,token:string): Observable<Employer> { 
    return this.http.get<Employer>(`${employerEndpoints.GET_EMPLOYER_BY_ID_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getEmployerByEmail(email: string,token:string): Observable<Employer> { 
    return this.http.get<Employer>(`${employerEndpoints.GET_EMPLOYER_BY_EMAIL_API}/${email}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createEmployer(employer: Employer,token:string): Observable<Employer> { 
    return this.http.post<Employer>(employerEndpoints.CREATE_EMPLOYER_API, employer,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  updateEmployer(employer: UpdateEmployerDTO,token:string): Observable<any> { 
    return this.http.put<any>(employerEndpoints.UPDATE_EMPLOYER_API, employer,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteEmployer(id: number,token:string): Observable<any> {
    return this.http.delete<any>(`${employerEndpoints.DELETE_EMPLOYER_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}

