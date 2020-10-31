import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../Models/api.response.model';
import { SignInModel } from '../Models/sign-in.model';
import { SignUpModel } from '../Models/sign-up.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  baseUrl = "/api/Account";

  SignUp(model: SignUpModel): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.baseUrl + '/register', model);
  }

  SignIn(model: SignInModel): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.baseUrl + '/login', model);
  }

  LogOut() {

  }

}
