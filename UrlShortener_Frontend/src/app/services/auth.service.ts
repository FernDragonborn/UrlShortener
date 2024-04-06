import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { jwtDecode } from 'jwt-decode';
import { enviroment } from '../enviroments/enviroment';
import { UserDto } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(private http: HttpClient) { }

  baseApiUrl: string = enviroment.baseApiUrl;

  login(user: UserDto): Observable<UserDto>{
    return this.http.post<UserDto>(this.baseApiUrl + '/api/auth/login', user);
  }

  renewToken(user: UserDto): Observable<UserDto>{
    return this.http.post<UserDto>(this.baseApiUrl + '/api/auth/renewToken', user);
  }

  register(user: UserDto): Observable<UserDto>{
    return this.http.post<UserDto>(this.baseApiUrl + '/api/auth/register', user);
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('jwtToken');

    if (token) {
      const decodedToken: any = jwtDecode(token);
      const tokenExpiration = new Date(decodedToken.exp * 1000);
      return tokenExpiration > new Date();
    } else {
      return false;
    }
  }
}
