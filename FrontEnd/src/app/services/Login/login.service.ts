import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/app/environment/API';
import { Login } from 'src/app/interfaces/ColaboradorLogin';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  private readonly API = environment.apiUrl

  constructor(private http: HttpClient) { }

  Logar(Login : Login) : Observable<Login>{
    return this.http.get(this.API + "/login")
  }
}





