import { Component, inject } from '@angular/core';
import { ActivatedRoute ,Router} from '@angular/router';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent {
  router : Router = inject(Router); 
  moveToSignup() {
    this.router.navigate(['/signup-jobseeker']);
  }
}
