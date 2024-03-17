import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {

  constructor(private router:Router, private jwtHelper: JwtHelperService){}
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const user = JSON.parse(localStorage.getItem("user"));

    if (user?.token && !this.jwtHelper.isTokenExpired(user?.token)){
      console.log(this.jwtHelper.decodeToken(user?.token));
      return true;
    }

    this.router.navigate(["/login"]);
    return false;
  }
}

export const OpenRoute = () => {
    const user = JSON.parse(localStorage.getItem("user"));
    const router = inject(Router);
    const toastr : ToastrService = inject(ToastrService);

    if (!user?.token){
        return true;
    }
    toastr.error("You are already Logged In...")
    router.navigate(["/profile"]);
    return false;  
}

export const SignUp = () => {
  const user = JSON.parse(localStorage.getItem("user"));
  const router = inject(Router);
  const toastr : ToastrService = inject(ToastrService);

  if (!user?.token){
      return true;
  }
  toastr.error("You have to logout First..")
  router.navigate(["/profile"]);
  return false;  
}

export const EmployerGuard = () => {
  const user = JSON.parse(localStorage.getItem("user"));
  const toastr : ToastrService = inject(ToastrService);
  const router = inject(Router);
  if(user?.token && user.role == "Employer"){
    return true;
  }
  toastr.error("404 UnAuthorized")
  router.navigate(['/home']);
  return false;

}

export const JobSeekerGuard = () => {
  const user = JSON.parse(localStorage.getItem("user"));
  const toastr : ToastrService = inject(ToastrService);
  const router = inject(Router);

  if(user?.token && user.role == "JobSeeker"){
    return true;
  }
  toastr.error("404 UnAuthorized")
  router.navigate(['/home']);
  return false;

}

export const GeneralGuard = () => {
  const user = JSON.parse(localStorage.getItem("user"));
  const toastr : ToastrService = inject(ToastrService);
  const router = inject(Router);
  if(user?.token){

    return true;
  }
  toastr.error("404 UnAuthorized")
  router.navigate(['/login']);
  return false;

}