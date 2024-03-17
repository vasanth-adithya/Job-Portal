import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SignupEmployerComponent } from './modules/signup-employer/signup-employer.component';
import { SignupJobseekerComponent } from './modules/signup-jobseeker/signup-jobseeker.component';
import { LoginComponent } from './modules/login/login.component';
import { ForgetPasswordComponent } from './modules/forget-password/forget-password.component';
import { ResetPasswordComponent } from './modules/reset-password/reset-password.component';
import { HomeComponent } from './modules/HomeModules/home/home.component';
import { ErrorComponent } from './shared/error/error.component';
import { AboutComponent } from './modules/about/about.component';
import { ProfileComponent } from './modules/profile/profile.component';
import { UpdateProfileComponent } from './modules/profile/update-profile/update-profile.component';
import { UpdateProfilejsComponent } from './modules/profile/update-profilejs/update-profilejs.component';
import { JoblistingsComponent } from './modules/joblistings/joblistings.component';
import { ApplicationsComponent } from './modules/applications/applications.component';
import { PostJoblistingsComponent } from './modules/joblistings/post-joblistings/post-joblistings.component';
import { UpdateJoblistingsComponent } from './modules/joblistings/update-joblistings/update-joblistings.component';
import { ViewJoblistingComponent } from './modules/joblistings/view-joblisting/view-joblisting.component';
import { ContactComponent } from './modules/contact/contact.component';
import { EmployerDashboardComponent } from './modules/employer-dashboard/employer-dashboard.component';
import { UpdateApplicationComponent } from './modules/employer-dashboard/update-application/update-application.component';
import { ListJobseekerComponent } from './modules/employer-dashboard/list-jobseeker/list-jobseeker.component';
import { AuthGuard, EmployerGuard, GeneralGuard, JobSeekerGuard, OpenRoute, SignUp } from './guards/auth.guard';

const routes: Routes = [

{path: "", component : HomeComponent},
{path:"jobseeker/view/:id",component:ListJobseekerComponent, canActivate:[GeneralGuard]},
{path:"application/update/:id",component:UpdateApplicationComponent, canActivate:[GeneralGuard,EmployerGuard]},
{path:"dashboard",component:EmployerDashboardComponent, canActivate:[GeneralGuard,EmployerGuard]},
{path:"contact",component:ContactComponent},
{path:"joblistings/view/:id",component:ViewJoblistingComponent},
{path:"joblistings/update/:id",component:UpdateJoblistingsComponent, canActivate:[GeneralGuard,EmployerGuard]},
{path:"joblistings/create", component:PostJoblistingsComponent, canActivate:[GeneralGuard,EmployerGuard]},
{path:'application', component:ApplicationsComponent,canActivate:[GeneralGuard,JobSeekerGuard]},
{path: 'profile', component: ProfileComponent, canActivate:[GeneralGuard] },
{path: "profile/update/:employerId", component:UpdateProfileComponent, canActivate:[GeneralGuard,EmployerGuard]},
{path: "profile/updatejs/:jobSeekerId", component:UpdateProfilejsComponent, canActivate:[GeneralGuard,JobSeekerGuard]},
{path: "joblistings", component:JoblistingsComponent},
{path:"about",component:AboutComponent},
{path: "home", component : HomeComponent},
{path:"signup-employer", component: SignupEmployerComponent,canActivate:[SignUp] },
{path:"signup-jobseeker", component: SignupJobseekerComponent,canActivate:[SignUp]},
{path:"login", component: LoginComponent,canActivate:[OpenRoute]},
{path: "forget-password", component: ForgetPasswordComponent,canActivate:[OpenRoute]},
{path: "reset-password/:token", component: ResetPasswordComponent,canActivate:[OpenRoute]},


{path: "**", component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
