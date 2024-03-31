import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { StorageService } from '../_services/storage.service';
import { filter } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  form: any = {
    username: null,
    password: null
  };
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];

  constructor(private authService: AuthService, private storageService: StorageService, private route:Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    const { username, password } = this.form;

    this.authService.login(username, password)
    .pipe(
      filter(data=> !! data)
    )
    .subscribe({
      next: data => {
        console.log(data);
        this.storageService.saveUser(data.userDetails);
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.route.navigate(['employee-list']);
      },
      error: err => {
        this.errorMessage = err.error;
        this.isLoginFailed = true;
      }
    });
  }


}
