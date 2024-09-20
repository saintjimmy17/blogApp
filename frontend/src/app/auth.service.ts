import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  http = inject(HttpClient)
  constructor() { }

  login(email:string, password: string) {
    return this.http.post<{
      accessToken: string;
    }>(environment.apiUrl + "/api/auth",{email, password});
  }

  get IsLoggedIn(){
    if(localStorage.getItem("token")) return true;
    else return false;
  }
}
