import { AuthService } from './../../services/auth.service';
import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { UserDto } from '../../models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})

export class LoginComponent {
  constructor(private authService: AuthService) {}

  user: UserDto = new UserDto();

  loginForm = new FormGroup({
    login: new FormControl(''),
    password: new FormControl(''),
  });

  login(user: UserDto): void {
    this.authService.login(this.user)
      .subscribe((userDto) => {
        this.user = userDto;
        localStorage.setItem('jwtToken', this.user.jwtToken);
        localStorage.setItem('userId', this.user.userId);
        console.log('logged in');
      })
  }

  renewToken(user: UserDto): void {
    this.authService.renewToken(this.user)
      .subscribe((userDto) => {
        this.user = userDto;
        localStorage.setItem('jwtToken', this.user.jwtToken);
        localStorage.setItem('userId', this.user.userId);
        console.log('token renewed');
    })
  }

  register(user: UserDto): void {
    this.authService.register(this.user)
      .subscribe((userDto) => {
        this.user = userDto;
        localStorage.setItem('jwtToken', this.user.jwtToken);
        localStorage.setItem('userId', this.user.userId);
        console.log('regitered new user');
      })
  }
}
