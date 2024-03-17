import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Resume } from '../models/Resume.Model';
import { resumeEndpoints } from './apis';
@Injectable({
  providedIn: 'root'
})
export class ResumeService {

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

  getAllResumes(token:string): Observable<Resume[]> {
    return this.http.get<Resume[]>(resumeEndpoints.GET_ALL_RESUMES_API,this.setToken(token))
    .pipe(
      catchError(this.handleError)
    );
  }

  getResumeById(id:number,token:string):Observable<Resume> {
    return this.http.get<Resume>(`${resumeEndpoints.GET_RESUME_BY_ID_API}/${id}`,this.setToken(token))
    .pipe(
      catchError(this.handleError)
    );
  }

  getResumeByJSId(id:number,token:string):Observable<any> {
    return this.http.get<Resume>(`${resumeEndpoints.GET_RESUME_BY_JSID_API}/${id}`,this.setToken(token))
    .pipe(
      catchError(this.handleError)
    );
  }

  createResume(resume: Resume,token:string): Observable<Resume> {
    return this.http.post<Resume>(resumeEndpoints.CREATE_RESUME_API, resume,this.setToken(token))
    .pipe(
      catchError(this.handleError)
    );
  }

  updateResume(resume: Resume,token:string): Observable<any> { 
    return this.http.put<any>(resumeEndpoints.UPDATE_RESUME_API, resume,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteResume(id: number,token:string): Observable<any> {
    return this.http.delete<any>(`${resumeEndpoints.DELETE_RESUME_API}/${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }

}
