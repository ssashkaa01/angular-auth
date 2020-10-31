import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { NgxSpinnerService } from 'ngx-spinner';
import { SignInModel } from '../Models/sign-in.model';
import { AuthService } from '../Services/AuthService.service';
import jwt_decode from "jwt-decode";

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {

  constructor(
    private spinner: NgxSpinnerService,
    private notifier: NotifierService,
    private authService: AuthService,
    private router: Router
  ) { }

  model = new SignInModel();

  login() {
    this.spinner.show();
    if (!this.model.isValid()) {
      this.spinner.hide();
      this.notifier.notify('error', 'Please, enter all fields');
    }
    else if (!this.model.isEmail()) {
      this.spinner.hide();
      this.notifier.notify('error', 'Please, enter correct email');
    }
    else {
      this.authService.SignIn(this.model).subscribe(
        data => {
          if (data.status === 200) {
            localStorage.setItem('token', data.token);
            var decode = jwt_decode(data.token);
            if (decode.roles === "Admin") {
              this.router.navigate(['/admin-panel'])
            }
            else if (decode.roles === "User") {
              this.router.navigate(['/client-panel'])
            }
          }
          else {
            for (var i = 0; i < data.errors.length; i++) {
              this.notifier.notify('error', data.errors[i])
            }
          }
          setTimeout(() => {
            this.spinner.hide();
          }, 1000);
        }
      )

    }
  }

  ngOnInit() {
  }

}
