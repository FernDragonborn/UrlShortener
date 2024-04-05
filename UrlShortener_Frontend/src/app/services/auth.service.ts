import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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
}
