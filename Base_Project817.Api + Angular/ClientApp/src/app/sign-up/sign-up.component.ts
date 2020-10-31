import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { NgxSpinnerService } from 'ngx-spinner';
import { SignUpModel } from '../Models/sign-up.model';
import { AuthService } from '../Services/AuthService.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  constructor(
    private spinner: NgxSpinnerService,
    private notifier: NotifierService,
    private authService: AuthService,
    private router: Router
  ) { }

  model = new SignUpModel();
  confirmPassword: string;

  register() {
    this.spinner.show();
    if (!this.model.isValid()) {
      this.spinner.hide();
      this.notifier.notify('error', 'Please, enter all fields');
    }
    else if (!this.model.isEmail()) {
      this.spinner.hide();
      this.notifier.notify('error', 'Please, enter correct email');
    }
    else if (this.model.Password != this.confirmPassword) {
      this.spinner.hide();
      this.notifier.notify('error', 'Password don\'t match');
    }
    else {
      this.authService.SignUp(this.model).subscribe(
        data => {
          if (data.status === 200) {
            this.notifier.notify('success', 'You success registered in system!');
            this.router.navigate(['/sign-in']);
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
