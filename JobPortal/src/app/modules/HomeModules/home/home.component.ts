// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-home',
//   templateUrl: './home.component.html',
//   styleUrl: './home.component.css'
// })
// export class HomeComponent {
//   user : any;
//   ngDoCheck(){
//     this.user = JSON.parse(localStorage.getItem('user'));
//   }
// }
import { Component, OnInit, AfterViewInit } from '@angular/core';
import Typewriter from 't-writer.js';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, AfterViewInit {
  user: any;
  writer: any;

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.initializeTypewriter();
  }

  ngDoCheck() {
    this.user = JSON.parse(localStorage.getItem('user'));
  }

  initializeTypewriter(): void {
    const target = document.querySelector('.tw');
    
    this.writer = new Typewriter(target, {
      typeColor: '#221E22',
    });

    if (this.user) {
      if(this.user.role === "Employer"){
      this.writer
        .type(`Hello ${this.user.employerName.toUpperCase()}, Welcome to Career Crafter...!!!  `)
        .rest(500)
        .start();
      }else{
        this.writer
        .type(`Hello ${this.user.jobSeekerName.toUpperCase()}, Welcome to Career Crafter...!!!  `)
        .rest(500)
        .start();
      }
    } else {
      this.writer.type(`Welcome to Career Crafter...!!!`).rest(500).start();
    }
  }
}