import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/app/environment/API';
import { Token } from 'src/app/interfaces/token';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  private readonly API = environment.apiUrl

  constructor(private http: HttpClient) { }

  Logar(Nm_Usuario: string, Ds_Senha: string) : Observable<Token>{
    return this.http.post<Token>(this.API + "login", {Nm_Usuario, Ds_Senha})
  }
}





