import { Component,inject } from '@angular/core';
import { ApplicationService } from '../../services/application.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-applications',
  templateUrl: './applications.component.html',
  styleUrl: './applications.component.css'
})
export class ApplicationsComponent {
  applcationService : ApplicationService = inject(ApplicationService);
  appList;
  loading : boolean=false;
  toastr : ToastrService = inject(ToastrService);
  user:any;
  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user'))
    this.loading=true;
    this.applcationService.getApplicationByJSId(JSON.parse(localStorage.getItem('user')).jobSeekerId,this.user.token).subscribe(
      (res) => {
        this.appList = res["data"] as any[];
        console.log(this.appList)
        console.log("api response: " + res);
        this.loading=false
      },
      error => {
        console.log(error.error);
        this.loading=false

        // this.toastr.error(error+"Login success")
      }
    );
}
  deleteApp(id:number){
    this.applcationService.deleteApplication(id,this.user.token).subscribe({
      next:data=>{
        this.toastr.success("Application Deleted Successfully")
        setTimeout(() => {
          location.reload();
      }, 500);
      },
      error:err => {
        console.log(err);
        this.toastr.error("Delete Failed")
      }
    })
  }
}
