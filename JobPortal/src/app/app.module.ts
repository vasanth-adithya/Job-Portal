import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './modules/login/login.component';
import { SignupEmployerComponent } from './modules/signup-employer/signup-employer.component';
import { SignupJobseekerComponent } from './modules/signup-jobseeker/signup-jobseeker.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { ForgetPasswordComponent } from './modules/forget-password/forget-password.component';
import { ResetPasswordComponent } from './modules/reset-password/reset-password.component';
import { HomeComponent } from './modules/HomeModules/home/home.component';
import { ErrorComponent } from './shared/error/error.component';
import { FooterComponent } from './shared/footer/footer.component';
import { AboutComponent } from './modules/about/about.component';
import { EmployerComponent } from './crud/employer/employer.component';
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
import { AuthGuard } from './guards/auth.guard';
import { JwtModule } from "@auth0/angular-jwt";

export function tokenGetter() { 
  return  JSON.parse(localStorage.getItem("user")).token; 
}
@NgModule({
  declarations: [AppComponent, LoginComponent, SignupEmployerComponent, SignupJobseekerComponent, NavbarComponent, ForgetPasswordComponent, ResetPasswordComponent, HomeComponent, ErrorComponent, FooterComponent, AboutComponent, EmployerComponent, ProfileComponent, UpdateProfileComponent, UpdateProfilejsComponent, JoblistingsComponent, ApplicationsComponent, PostJoblistingsComponent, UpdateJoblistingsComponent, ViewJoblistingComponent, ContactComponent, EmployerDashboardComponent, UpdateApplicationComponent, ListJobseekerComponent],
  imports: [BrowserModule, AppRoutingModule, FormsModule, ToastrModule.forRoot(),BrowserAnimationsModule,HttpClientModule,ReactiveFormsModule,CommonModule,    JwtModule.forRoot({
    config: {
      tokenGetter: tokenGetter,
      allowedDomains: ['*'],
      disallowedRoutes: []
    }
  })
],

  
  
  providers: [AuthGuard],
  bootstrap: [AppComponent],
})
export class AppModule {}
