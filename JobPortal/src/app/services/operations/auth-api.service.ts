import { Injectable, inject } from '@angular/core';
import { endpoints } from '../apis';
import { catchError, finalize, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { RegisterEmployerDTO } from '../../models/DTO/RegisterEmployerDTO';
import { RegisterJobSeekerDTO } from '../../models/DTO/RegisterJobSeekerDTO';
import { Employer } from '../../models/Employer.Model';
import { JobSeeker } from '../../models/JobSeeker.Model';
import { ResetPasswordDTO } from '../../models/DTO/ResetPasswordDTO';


@Injectable({
  providedIn: 'root'
})

export class AuthApiService {

  http: HttpClient = inject(HttpClient);
  employer = new BehaviorSubject<Employer>(null);
  jobseeker = new BehaviorSubject<JobSeeker>(null);
  toastr : ToastrService = inject(ToastrService);
  router : Router = inject(Router);
  private tokenExpiretimer : any;

  signupEmployer (user : RegisterEmployerDTO) {
    const loadingToast = this.toastr.info('Signing up...', 'Please wait', {
      // disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    return this.http.post(endpoints.SIGNUP_EMPLOYER_API, user).pipe(
      catchError(error => {
        if (loadingToast) {
          this.toastr.clear();
        }
        this.toastr.error('Failed to sign up', 'Error');
        
        return throwError(() => error);
      }),
      finalize(() => {
        if (loadingToast) {
          this.toastr.clear();
        }
      })
    );
  }
  signupJobSeeker(user : RegisterJobSeekerDTO) {
    const loadingToast = this.toastr.info("Signing up ", "Please wait...", {
      // disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    return this.http.post(endpoints.SIGNUP_JOBSEEKER_API, user).pipe(
      catchError(error => {
        if (loadingToast) {
          this.toastr.clear();
        }
        this.toastr.error('Failed to sign up', 'Error');
        
        return throwError(() => error);
      }),
      finalize(() => {
        if (loadingToast) {
          this.toastr.clear();
        }
      })
    );
  }
  
  loginEmployer(email:string, password:string){

    const loadingToast = this.toastr.info("Logging in", "Please wait...", {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    const data = {email: email, password: password};

    return this.http.post(
      endpoints.LOGIN_EMPLOYER_API, data
      ).pipe(catchError((error) => {
        if (loadingToast) {
          this.toastr.clear();
        }
        this.toastr.error('Invalid Credentials', 'Error');

        return throwError(() => error);
      }), tap((res) => {
          this.handleCreateUser(res)
      }),finalize(() => {
        if (loadingToast) {
          this.toastr.clear();
        }
      }));
  }

  loginJobSeeker(email:string, password:string){

    const loadingToast = this.toastr.info("Logging in", "Please wait...", {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    const data = {email: email, password: password};

    return this.http.post(
      endpoints.LOGIN_JOBSEEKER_API, data
      ).pipe(catchError((error) => {
        if (loadingToast) {
          this.toastr.clear();
        }
        this.toastr.error('Failed to Login', 'Error');
          
        return throwError(() => error);
      }), tap((res) => {
          localStorage.setItem("user" , JSON.stringify(res));
          this.handleCreateUser(res)
      }),finalize(() => {
        if (loadingToast) {
          this.toastr.clear();
        }
      }));
  }

  autoLogin () {
    const res = JSON.parse(localStorage.getItem('user'));

    if(res === null || res === undefined){
        return;
    }
    let user : any;

    if(res?.role == "Employer") {

      user = new Employer
      (
        res.employerId,
        res.employerName,
        res.userName,
        res.email,
        res.password,
        res.gender,
        res.companyName,
        res.contactPhone,
        res.cwebsiteUrl,
        res.expiresIn,
        res.role,
        res.token,
        res.resetPasswordExpires
        
      );
    }
      else{
        user = new JobSeeker(
          res.jobSeekerId,
          res.jobSeekerName,
          res.userName,
          res.email,
          res.password,
          res.gender,
          res.contactPhone,
          res.address,
          res.description,
          res.dateOfBirth,
          res.qualification,
          res.specialization,
          res.institute,
          res.year,
          res.cgpa,
          res.companyName,
          res.position,
          res.responsibilities,
          res.startDate,
          res.endDate,
          res.expiresIn,
          res.role,
          res.token,
          res.resetPasswordExpires
        );
      }
      if(user.token){
        this.jobseeker.next(user);
        const timerValue = user?.expiresIn.getTime() - new Date().getTime();
        this.autoLogout(timerValue);
      }
    }
    logout (token : string) {
      console.log(token)
      const httpOptions = {
      headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    })
  };
  this.jobseeker.next(null);
    this.router.navigate(['/login']);
    localStorage.removeItem('user');
    sessionStorage.removeItem('user');

    if(this.tokenExpiretimer){
        clearTimeout(this.tokenExpiretimer);
    }
    this.tokenExpiretimer = null;


   return this.http.post(endpoints.LOGOUT_API, null, httpOptions).pipe(
    catchError(error => {
      this.toastr.error('Failed to logout', 'Error');
      
      return throwError(() => error);
    }))
    }
  
autoLogout(expireTime: number){
  const res = JSON.parse(localStorage.getItem('user'));
  this.tokenExpiretimer = setTimeout(() => {
      this.logout(res?.token);
  }, (3*60*60*1000));
}
forgetPassword(email:string){

  const loadingToast = this.toastr.info("Sending Email", "Please wait...", {
    disableTimeOut: true,
    closeButton: false,
    positionClass: 'toast-top-center'
  });

  const data = {email: email};

  return this.http.post(
  endpoints.FORGETPASSWORD_API, data
  ).pipe(catchError((error) => {
    if (loadingToast) {
      this.toastr.clear();
    }
    this.toastr.error('Failed to sent email', 'Error');
      
    return throwError(() => error);
  }), tap((res) => {
      console.log(res);
  }),finalize(() => {
    if (loadingToast) {
      this.toastr.clear();
    }
  }));
}

resetPassword(data : ResetPasswordDTO){

  const loadingToast = this.toastr.info("Confirming Details", "Please wait...", {
    disableTimeOut: true,
    closeButton: false,
    positionClass: 'toast-top-center'
  });
  
  return this.http.post(
  endpoints.RESETPASSWORD_API, data
  ).pipe(catchError((error) => {
    if (loadingToast) {
      this.toastr.clear();
    }
    this.toastr.error('Failed to reset password', 'Error');
      
    return throwError(() => error);
  }), tap((res) => {
      console.log(res);
  }),finalize(() => {
    if (loadingToast) {
      this.toastr.clear();
    }
  }));
}

  private handleCreateUser(res){
    const expiresInTs = new Date().getTime() + ((3 * 60 * 60) * 1000);
    const expiresIn = new Date(expiresInTs);
    let user : any;

    if(res.user.role == "Employer") {
      user = new Employer(
        res.user.employerId,
        res.user.employerName,
        res.user.userName,
        res.user.email,
        res.user.password,
        res.user.gender,
        res.user.companyName,
        res.user.contactPhone,
        res.user.cwebsiteUrl,
        expiresIn,
        res.user.role,
        res.user.token,
        res.user.resetPasswordExpires
        
      );
      this.employer.next(user);
    }
      else{
        user = new JobSeeker(
          res.user.jobSeekerId,
          res.user.jobSeekerName,
          res.user.userName,
          res.user.email,
          res.user.password,
          res.user.gender,
          res.user.contactPhone,
          res.user.address,
          res.user.description,
          res.user.dateOfBirth,
          res.user.qualification,
          res.user.specialization,
          res.user.institute,
          res.user.year,
          res.user.cgpa,
          res.user.companyName,
          res.user.position,
          res.user.responsibilities,
          res.user.startDate,
          res.user.endDate,
          expiresIn,
          res.user.role,
          res.user.token,
          res.user.resetPasswordExpires,
        );
      this.jobseeker.next(user);
        }
   
      // this.user.next(user);
      this.autoLogout(user.expiresIn * 1000);
  
      localStorage.setItem("user" , JSON.stringify(user));

  }

}

