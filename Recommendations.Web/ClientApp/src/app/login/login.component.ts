import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserService} from "../../common/services/user/user.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  error?: string

  constructor(private http: HttpClient,
              private router: Router,
              private userService: UserService) {

  }

  loginForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(4)
    ]),
    remember: new FormControl(false)
  })

  onSubmit() {
    this.http.post('api/user/login', this.loginForm.value)
      .subscribe({
        next: () => {
          this.userService.isAuthenticated = true
          this.router.navigate(['/create-review'])
        },
        error: err => {
          if (err.status === 404)
            this.error = 'User with this email does not exist'
          if (err.status === 401)
            this.error = 'Invalid password'
        }
      })
  }

}
