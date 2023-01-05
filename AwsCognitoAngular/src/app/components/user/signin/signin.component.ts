import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  @ViewChild('usrForm') form: NgForm;
  didFail = false;
  isLoading = false;
  constructor(private authService: AuthService) {
  }

  ngOnInit() {
    this.authService.authIsLoading.subscribe(
      (isLoading: boolean) => this.isLoading = isLoading
    );
    this.authService.authDidFail.subscribe(
      (didFail: boolean) => this.didFail = didFail
    );
  }

  onSubmit() {
    const usrName = this.form.value.username;
    const password = this.form.value.password;
    this.authService.signIn(usrName, password);
  }
}
